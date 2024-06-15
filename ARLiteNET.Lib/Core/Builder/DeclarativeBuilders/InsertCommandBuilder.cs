using ARLiteNET.Lib.Common;
using ARLiteNET.Lib.Core.ExpressionHelpers;
using ARLiteNET.Lib.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace ARLiteNET.Lib.Core
{
    public class InsertCommandBuilder<T> : IDbCommandBuilder
    {
        private bool _hasColumnQueryInfos => _columnQueryInfos.Count > 0;
        private readonly List<InsertColumnQuery> _columnQueryInfos;

        private readonly T _instance;
        private readonly AdoCommandBuilder _adoCommandbuilder;
        private readonly IInsertQueryBuilder _insertQueryBuilder;

        public InsertCommandBuilder(T instance, AdoCommandBuilder adoBuilder,
                                                IInsertQueryBuilder insertQueryBuilder)
        {
            this._instance = instance ?? throw new ArgumentNullException(nameof(instance));
            this._adoCommandbuilder = adoBuilder ?? throw new ArgumentNullException(nameof(adoBuilder));     
            this._insertQueryBuilder = insertQueryBuilder ?? throw new ArgumentNullException(nameof(insertQueryBuilder)); ;

            _columnQueryInfos = new List<InsertColumnQuery>();
        }

        public InsertColumnQuery Column(string columnName)
        {
            InsertColumnQuery columnInfo = new InsertColumnQuery(columnName);
            _columnQueryInfos.Add(columnInfo);

            return columnInfo;
        }

        public InsertColumnQuery Column<TMember>(Expression<Func<T, TMember>> member)
        {
            ExpressionMember expMember = ExpressionMemberFactory.Create(member);

            InsertColumnQuery columnInfo = new InsertColumnQuery(expMember.EndPointName);
            _columnQueryInfos.Add(columnInfo);

            return columnInfo;
        }

        IDbCommand IDbCommandBuilder.Build()
        {
            //if (_hasColumnQueryInfos)
            //{

            //}

            //_insertQueryBuilder.Value()

             string queryStr = _insertQueryBuilder.Build();
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
