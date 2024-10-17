using ARLiteNET.Common;
using ARLiteNET.Core;
using ARLiteNET.Exceptions;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Linq;
using ARLiteNET.Core.Mappers;

namespace ARLiteNET
{
    public class UpdateCommandBuilder<T> : IDbCommandBuilder
    {
        private readonly AdoCommandBuilder _adoCommandBuilder;
        private readonly IUpdateQueryBuilder _updateQueryBuilder;
        private readonly T _instance;
        private bool _hasColumnQueryInfos => _columnQueryInfos.Count > 0;
        private readonly List<UpdateColumnQuery> _columnQueryInfos;
        
        public UpdateCommandBuilder(T instance, AdoCommandBuilder adoCommandBuilder,
                                    IUpdateQueryBuilder updateQueryBuilder)
        {
            _instance = instance ?? throw new ArgumentNullException(nameof(instance));
            _adoCommandBuilder = adoCommandBuilder ?? throw new ArgumentNullException(nameof(adoCommandBuilder));
            _updateQueryBuilder = updateQueryBuilder ?? throw new ArgumentNullException(nameof(updateQueryBuilder));

            _columnQueryInfos = new List<UpdateColumnQuery>();
        }

        public UpdateColumnQuery Column(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ARLiteException(nameof(UpdateCommandBuilder<T>), nameof(Column),
                                new Exception($"{columnName} column can not be null!"));

            UpdateColumnQuery columnInfo = new UpdateColumnQuery(columnName);
            _columnQueryInfos.Add(columnInfo);

            return columnInfo;
        }

        public UpdateColumnQuery Column<TMember>(Expression<Func<T, TMember>> member)
        {
            if (member is null)
                throw new ARLiteException(nameof(UpdateCommandBuilder<T>), nameof(Column),
                                new Exception($"Selected member can not be null!"));

            ExpressionMember expMember = ExpressionMember.Create(member);

            if (!expMember.IsFieldOrProperty)
                throw new ARLiteException(nameof(UpdateCommandBuilder<T>), nameof(Column),
                                            new Exception("The class member is not property or field!"));

            return Column(expMember.Name);
        }

        IDbCommand IDbCommandBuilder.Build()
        {
            DataType _getUpdateDataType(Type type)
            {
                Type memberType = Nullable.GetUnderlyingType(type) != null ?
                                    Nullable.GetUnderlyingType(type) : type;

                if (memberType.Equals(typeof(byte))
                    || memberType.Equals(typeof(sbyte))
                    || memberType.Equals(typeof(decimal))
                    || memberType.Equals(typeof(double))
                    || memberType.Equals(typeof(float)))
                {
                    return DataType.REAL;
                }
                else if (memberType.Equals(typeof(int))
                            || memberType.Equals(typeof(uint))
                            || memberType.Equals(typeof(long))
                            || memberType.Equals(typeof(ulong))
                            || memberType.Equals(typeof(short))
                            || memberType.Equals(typeof(ushort)))
                {
                    return DataType.INTEGER;
                }
                else if (memberType.Equals(typeof(bool)))
                {
                    return DataType.BOOLEAN;
                }
                else if (memberType.Equals(typeof(string))
                            || memberType.Equals(typeof(DateTime)))
                {
                    return DataType.TEXT;
                }

                return DataType.NULL;
            }
            IDbCommand _BuildCommand(IQueryBuilder queryBuilderInstance)
            {
                string queryStr = queryBuilderInstance.Build();

                _adoCommandBuilder.SetCommand(queryStr);
                return ((IDbCommandBuilder)_adoCommandBuilder).Build();
            }

            var mapType = Mapper.MapInstance(_instance);

            if (mapType is null)
                throw new ARLiteException(nameof(UpdateCommandBuilder<T>), nameof(IDbCommandBuilder.Build),
                                                new Exception($"{typeof(T).Name} is not suitable for mapping!"));
            if (!mapType.HasMembers)
                throw new ARLiteException(nameof(UpdateCommandBuilder<T>), nameof(IDbCommandBuilder.Build),
                                                new Exception($"{typeof(T).Name} has not any member for mapping!"));

            IEnumerable<MapMember> mappedTypeMembers = mapType.Members;

            if (_hasColumnQueryInfos)
            {
                var ignoreMembers = _columnQueryInfos.Where(x => x.OperationName == nameof(UpdateColumnQuery.Ignore));

                mappedTypeMembers = mappedTypeMembers.Where(x => !ignoreMembers.Any(y => y.ColumnName == x.Name));
            }

            foreach (var member in mappedTypeMembers)
            {
                DataType dbDataType = _getUpdateDataType(member.Type);

                _updateQueryBuilder.Set(new ColumnValueObject(member.Name, member.Value, dbDataType));
            }

            var conditionColumns = _columnQueryInfos.Where(x => x.OperationName == nameof(UpdateColumnQuery.EqualTo)
                                                             || x.OperationName == nameof(UpdateColumnQuery.NotEqualTo));

            if (conditionColumns != null && conditionColumns.Any())
            {
                object queryBuilderInstance = null;

                foreach (var columnInfo in conditionColumns)
                {
                    if (queryBuilderInstance is null)
                    {
                        queryBuilderInstance = _updateQueryBuilder.Where(columnInfo.ColumnName);
                        queryBuilderInstance = InvokeQueryBuilder(queryBuilderInstance, columnInfo);
                    }
                    else
                    {
                        queryBuilderInstance = InvokeQueryBuilder(queryBuilderInstance, columnInfo);
                    }
                }

                return _BuildCommand(queryBuilderInstance as IQueryBuilder);
            }

            return _BuildCommand(_updateQueryBuilder);
        }

        private object InvokeQueryBuilder(object instance, UpdateColumnQuery columnInfo)
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

        public class UpdateColumnQuery
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

            internal UpdateColumnQuery(string columnName)
            {
                if (columnName == null)
                    throw new ARLiteException(nameof(UpdateColumnQuery), string.Empty,
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

            public void NotEqualTo(string value, bool and = true)
            {
                _type = typeof(string);
                _operationName = nameof(NotEqualTo);
                _value = value;
                _and = and;
            }

            public void NotEqualTo(decimal value, bool and = true)
            {
                _type = typeof(decimal);
                _operationName = nameof(NotEqualTo);
                _value = value;
                _and = and;
            }

            public void NotEqualTo(int value, bool and = true)
            {
                _type = typeof(int);
                _operationName = nameof(NotEqualTo);
                _value = value;
                _and = and;
            }

            public void Ignore() => _operationName = nameof(Ignore);
        }
    }
}
