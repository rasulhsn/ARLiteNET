using ARLiteNET.Common;
using ARLiteNET.Core;
using ARLiteNET.Exceptions;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Linq;

namespace ARLiteNET
{
    public class DeleteCommandBuilder<T> : IDbCommandBuilder
    {
        private readonly AdoCommandBuilder _adoCommandBuilder;
        private readonly IDeleteQueryBuilder _deleteQueryBuilder;
        private readonly List<DeleteColumnQuery> _columnQueryInfos;

        public DeleteCommandBuilder(AdoCommandBuilder adoCommandBuilder,
                                    IDeleteQueryBuilder deleteQueryBuilder)
        {
            _adoCommandBuilder = adoCommandBuilder ?? throw new ArgumentNullException(nameof(adoCommandBuilder));
            _deleteQueryBuilder = deleteQueryBuilder ?? throw new ArgumentNullException(nameof(deleteQueryBuilder));

            _columnQueryInfos = new List<DeleteColumnQuery>();
        }

        public DeleteColumnQuery Column(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ARLiteException(nameof(DeleteCommandBuilder<T>), nameof(Column),
                                new Exception($"{columnName} column can not be null!"));

            DeleteColumnQuery columnInfo = new DeleteColumnQuery(columnName);
            _columnQueryInfos.Add(columnInfo);

            return columnInfo;
        }

        public DeleteColumnQuery Column<TMember>(Expression<Func<T, TMember>> member)
        {
            if (member is null)
                throw new ARLiteException(nameof(DeleteCommandBuilder<T>), nameof(Column),
                                new Exception($"Selected member can not be null!"));

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

            var conditionColumns = _columnQueryInfos.Where(x => x.OperationName == nameof(DeleteColumnQuery.EqualTo));

            if (conditionColumns != null && conditionColumns.Any())
            {
                object queryBuilderInstance = null;

                foreach (var columnInfo in conditionColumns)
                {
                    if (queryBuilderInstance is null)
                    {
                        queryBuilderInstance = _deleteQueryBuilder.Where(columnInfo.ColumnName);
                        queryBuilderInstance = InvokeQueryBuilder(queryBuilderInstance, columnInfo);
                    }
                    else
                    {
                        queryBuilderInstance = InvokeQueryBuilder(queryBuilderInstance, columnInfo);
                    }
                }

                return _BuildCommand(queryBuilderInstance);
            }

            return _BuildCommand(_deleteQueryBuilder);
        }

        private object InvokeQueryBuilder(object instance, DeleteColumnQuery columnInfo)
        {
            void DefaultInvoke()
            {
                var methodInfo = instance.GetType().GetMethod(columnInfo.OperationName, new Type[] { columnInfo.Type });
                instance = methodInfo.Invoke(instance, new object[] { columnInfo.Value });
            }

            if (instance is IConditionalFunctionQueryBuilder)
            {
                DefaultInvoke();
            }
            else if (instance is IConditionQueryBuilder<IConditionalFunctionQueryBuilder>)
            {
                if (columnInfo.And)
                {
                    instance = ((IConditionQueryBuilder<IConditionalFunctionQueryBuilder>)instance).And(columnInfo.ColumnName);
                }
                else
                {
                    instance = ((IConditionQueryBuilder<IConditionalFunctionQueryBuilder>)instance).Or(columnInfo.ColumnName);
                }

                DefaultInvoke();
            }
            else
            {
                throw new InvalidOperationException("");
            }

            return instance;
        }

        public class DeleteColumnQuery
        {
            private readonly string _columnName;
            private object _value;
            private string _operationName;
            private bool _and;
            private Type _type;

            internal Type Type => _type;
            internal string ColumnName => _columnName;
            internal string OperationName => _operationName;
            internal object Value => _value;
            internal bool And => _and;

            internal DeleteColumnQuery(string columnName)
            {
                if (columnName == null)
                    throw new ARLiteException(nameof(DeleteColumnQuery), string.Empty,
                            new ArgumentNullException(nameof(columnName)));

                _columnName = columnName;
            }

            public void EqualTo(string value, bool and = true)
            {
                _type = typeof(string);
                _operationName = nameof(EqualTo);
                _value = value;
                _and = and;
            }

            public void EqualTo(decimal value, bool and = true)
            {
                _type = typeof(decimal);
                _operationName = nameof(EqualTo);
                _value = value;
                _and = and;
            }

            public void EqualTo(int value, bool and = true)
            {
                _type = typeof(int);
                _operationName = nameof(EqualTo);
                _value = value;
                _and = and;
            }
        }
    }
}
