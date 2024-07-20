using ARLiteNET.Lib.Common;
using ARLiteNET.Lib.Core.Mappers;
using ARLiteNET.Lib.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace ARLiteNET.Lib.Core
{
    public class InsertCommandBuilder<T> : IDbCommandBuilder
    {
        private bool _hasColumnQueryInfos => _columnQueryInfos.Count > 0;
        private readonly Dictionary<string, InsertColumnQuery> _columnQueryInfos;

        private readonly T _instance;
        private readonly AdoCommandBuilder _adoCommandbuilder;
        private readonly IInsertQueryBuilder _insertQueryBuilder;

        public InsertCommandBuilder(T instance, AdoCommandBuilder adoBuilder,
                                                IInsertQueryBuilder insertQueryBuilder)
        {
            this._instance = instance ?? throw new ArgumentNullException(nameof(instance));
            this._adoCommandbuilder = adoBuilder ?? throw new ArgumentNullException(nameof(adoBuilder));     
            this._insertQueryBuilder = insertQueryBuilder ?? throw new ArgumentNullException(nameof(insertQueryBuilder)); ;

            _columnQueryInfos = new Dictionary<string, InsertColumnQuery>();
        }

        public InsertColumnQuery Column(string columnName)
        {
            if (_columnQueryInfos.ContainsKey(columnName))
                throw new ARLiteException(nameof(InsertCommandBuilder<T>), nameof(Column),
                                new Exception($"{columnName} column is already exists!"));

            InsertColumnQuery columnInfo = new InsertColumnQuery(columnName);
            _columnQueryInfos.Add(columnName, columnInfo);

            return columnInfo;
        }

        public InsertColumnQuery Column<TMember>(Expression<Func<T, TMember>> member)
        {
            ExpressionMember expMember = ExpressionMember.Create(member);

            if (!expMember.IsFieldOrProperty)
                throw new ARLiteException(nameof(InsertCommandBuilder<T>), nameof(Column),
                                            new Exception("The class member is not property or field!"));

            return Column(expMember.Name);
        }

        IDbCommand IDbCommandBuilder.Build()
        {
            InsertDataType _getInsertDataType(Type type)
            {
                Type memberType = Nullable.GetUnderlyingType(type) != null ?
                                    Nullable.GetUnderlyingType(type) : type;

                if (memberType.Equals(typeof(byte))
                    || memberType.Equals(typeof(sbyte))
                    || memberType.Equals(typeof(decimal))
                    || memberType.Equals(typeof(double))
                    || memberType.Equals(typeof(float)))
                {
                    return InsertDataType.REAL;
                }
                else if (memberType.Equals(typeof(int))
                            || memberType.Equals(typeof(uint))
                            || memberType.Equals(typeof(long))
                            || memberType.Equals(typeof(ulong))
                            || memberType.Equals(typeof(short))
                            || memberType.Equals(typeof(ushort)))
                {
                    return InsertDataType.INTEGER;
                }
                else if (memberType.Equals(typeof(bool)))
                {
                    return InsertDataType.BOOLEAN;
                }
                else if (memberType.Equals(typeof(string))
                            || memberType.Equals(typeof(DateTime)))
                {
                    return InsertDataType.TEXT;
                }

                return InsertDataType.NULL;
            }

            var mapType = Mapper.MapInstance(_instance);

            if (mapType is null)
                throw new ARLiteException(nameof(InsertCommandBuilder<T>), nameof(IDbCommandBuilder.Build),
                                                new Exception($"{typeof(T).Name} is not suitable for mapping!"));
            if (!mapType.HasMembers)
                throw new ARLiteException(nameof(InsertCommandBuilder<T>), nameof(IDbCommandBuilder.Build),
                                                new Exception($"{typeof(T).Name} has not any member for mapping!"));

            IEnumerable<MapMember> mappedTypeMembers = mapType.Members;

            if (_hasColumnQueryInfos)
            {
                var ignoreMembers = _columnQueryInfos.Where(x => x.Value.OperationName == nameof(InsertColumnQuery.Ignore))
                                                     .Select(x => x.Key);

                mappedTypeMembers = mappedTypeMembers.Where(x => !ignoreMembers.Contains(x.Name));
            }

            InsertValueObject[] insertObjects = new InsertValueObject[mappedTypeMembers.Count()];
            int counter = 0;

            foreach (var member in mappedTypeMembers)
            {
                InsertDataType dbDataType = _getInsertDataType(member.Type);
                insertObjects[counter] = new InsertValueObject(member.Name, member.Value, dbDataType);

                counter++;
            }

            string queryStr = _insertQueryBuilder.Value(insertObjects)
                                                  .Build();
            _adoCommandbuilder.SetCommand(queryStr);

            return ((IDbCommandBuilder)_adoCommandbuilder).Build();
        }

        public class InsertColumnQuery
        {
            private readonly string _columnName;
            private string _operationName;

            internal string ColumnName => _columnName;
            internal string OperationName => _operationName;

            internal InsertColumnQuery(string columnName)
            {
                if (columnName == null)
                    throw new ARLiteException(nameof(InsertColumnQuery), string.Empty,
                            new ArgumentNullException(nameof(columnName)));

                _columnName = columnName;
            }

            public void Ignore()
            {
                _operationName = nameof(Ignore);
            }
        }
    }
}
