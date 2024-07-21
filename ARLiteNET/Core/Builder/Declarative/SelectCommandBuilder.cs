using ARLiteNET.Common;
using ARLiteNET.Core;
using ARLiteNET.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace ARLiteNET
{
    public class SelectCommandBuilder<T> : IDbCommandBuilder
    {
        private readonly string _tableName;
        private readonly AdoCommandBuilder _adoCommandBuilder;
        private readonly ISelectQueryBuilder _selectQueryBuilder;
        private readonly List<SelectColumnQuery> _columnQueryInfos;

        public SelectCommandBuilder(string tableName,
                            AdoCommandBuilder adoCommandBuilder,
                            ISelectQueryBuilder selectQueryBuilder)
        {
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            _selectQueryBuilder = selectQueryBuilder ?? throw new ArgumentNullException(nameof(selectQueryBuilder));
            _adoCommandBuilder = adoCommandBuilder ?? throw new ArgumentNullException(nameof(adoCommandBuilder));

            _columnQueryInfos = new List<SelectColumnQuery>();
        }

        public SelectColumnQuery Column(string columnName)
        {
            SelectColumnQuery columnInfo = new SelectColumnQuery(columnName);
            _columnQueryInfos.Add(columnInfo);

            return columnInfo;
        }

        public SelectColumnQuery Column<TMember>(Expression<Func<T, TMember>> member)
        {
            ExpressionMember expMember = ExpressionMember.Create(member);
            
            if (!expMember.IsFieldOrProperty)
                throw new ARLiteException(nameof(SelectCommandBuilder<T>), nameof(Column),
                                            new Exception("The class member is not property or field!"));

            return Column(expMember.Name);
        }

        IDbCommand IDbCommandBuilder.Build()
        {
            IDbCommand _BuildCommand(object instance)
            {
                string queryStr = ((IQueryBuilder)instance).Build();
                _adoCommandBuilder.SetCommand(queryStr);

                return ((IDbCommandBuilder)_adoCommandBuilder).Build();
            }

            var selectColumns = _columnQueryInfos.Where(x => x.OperationName == nameof(SelectColumnQuery.Only))
                                                    .Select(x => x.ColumnName);

            IFromQueryBuilder fromQueryBuilder = null;

            if (selectColumns != null && selectColumns.Any())
            {
                fromQueryBuilder = _selectQueryBuilder.Select(selectColumns)
                                                    .From(_tableName);
            }
            else
            {
                fromQueryBuilder = _selectQueryBuilder.From(_tableName);
            }

            var conditionColumns = _columnQueryInfos.Where(x => x.OperationName == nameof(SelectColumnQuery.EqualTo)
                                                          || x.OperationName == nameof(SelectColumnQuery.Len));

            if (conditionColumns != null && conditionColumns.Any())
            {
                object queryBuilderInstance = null;

                foreach (var columnInfo in conditionColumns)
                {
                    if (queryBuilderInstance is null)
                    {
                        queryBuilderInstance = fromQueryBuilder.Where(columnInfo.ColumnName);
                        queryBuilderInstance = InvokeQueryBuilder(queryBuilderInstance, columnInfo);
                    }
                    else
                    {
                        queryBuilderInstance = InvokeQueryBuilder(queryBuilderInstance, columnInfo);
                    }
                }

                return _BuildCommand(queryBuilderInstance);
            }

            return _BuildCommand(fromQueryBuilder);
        }

        private object InvokeQueryBuilder(object instance, SelectColumnQuery columnInfo)
        {
            void DefaultInvoke()
            {
                var methodInfo = instance.GetType().GetMethod(columnInfo.OperationName);
                instance = methodInfo.Invoke(instance, new object[] { columnInfo.Value });
            }

            if (instance is IWhereQueryBuilder)
            {
                DefaultInvoke();
            }
            else if (instance is IConditionQueryBuilder<IWhereQueryBuilder>)
            {
                if (columnInfo.And)
                {
                    instance = ((IConditionQueryBuilder<IWhereQueryBuilder>)instance).And(columnInfo.ColumnName);
                }
                else
                {
                    instance = ((IConditionQueryBuilder<IWhereQueryBuilder>)instance).Or(columnInfo.ColumnName);
                }

                DefaultInvoke();
            }
            else
            {
                throw new InvalidOperationException("");
            }

            return instance;
        }

        public class SelectColumnQuery
        {
            private readonly string _columnName;
            private object _value;
            private string _operationName;
            private bool _and;

            internal string ColumnName => _columnName;
            internal string OperationName => _operationName;
            internal object Value => _value;
            internal bool And => _and;

            internal SelectColumnQuery(string columnName)
            {
                if (columnName == null)
                    throw new ARLiteException(nameof(SelectColumnQuery), string.Empty,
                            new ArgumentNullException(nameof(columnName)));

                _columnName = columnName;
            }

            public void EqualTo(string value, bool and = true)
            {
                _operationName = nameof(EqualTo);
                _value = value;
                _and = and;
            }

            public void Len(int value, bool and = true)
            {
                _operationName = nameof(Len);
                _value = value;
                _and = and;
            }

            public void Only()
            {
                _operationName = nameof(Only);
                _value = null;
            }
        }
    }
}
