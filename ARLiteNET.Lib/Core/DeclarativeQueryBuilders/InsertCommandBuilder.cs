using ARLiteNET.Lib.Common;
using ARLiteNET.Lib.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;

namespace ARLiteNET.Lib.Core
{
    public class InsertCommandBuilder<T> : IDbCommandBuilder
    {
        private bool _hasColumnQueryInfos => _columnQueryInfos.Count > 0;
        private readonly List<InsertColumnQuery> _columnQueryInfos;

        private readonly AdoCommandBuilder _builder;
        private readonly T _instance;
        private readonly IInsertQueryBuilder _insertQueryBuilder;

        public InsertCommandBuilder(T instance, AdoCommandBuilder builder,
                                                IInsertQueryBuilder insertQueryBuilder)
        {
            this._instance = instance ?? throw new ArgumentNullException(nameof(instance));
            this._builder = builder ?? throw new ArgumentNullException(nameof(builder));     
            this._insertQueryBuilder = insertQueryBuilder ?? throw new ArgumentNullException(nameof(insertQueryBuilder)); ;

            _columnQueryInfos = new List<InsertColumnQuery>();
        }

        public InsertColumnQuery Column(string columnName)
        {
            InsertColumnQuery columnInfo = new InsertColumnQuery(columnName);
            _columnQueryInfos.Add(columnInfo);

            return columnInfo;
        }

        IDbCommand IDbCommandBuilder.Build()
        {
            //if (_hasColumnQueryInfos)
            //{

            //}

            throw new System.NotImplementedException();
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
                    throw new ARLiteException(nameof(InsertColumnQuery),
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
