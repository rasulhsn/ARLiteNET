using System.Collections.Generic;
using System.Data;

namespace ARLiteNET.Lib.Core
{
    public class InsertCommandBuilder<T> : IDbCommandBuilder
    {
        private readonly List<InsertColumnQuery> _columnQueryInfos;

        public InsertCommandBuilder()
        {
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
            throw new System.NotImplementedException();
        }

        public class InsertColumnQuery
        {
            private readonly string _columnName;
            private object _value;
            private string _operationName;
            private bool _and;

            internal string ColumnName => _columnName;
            internal string OperationName => _operationName;
            internal object Value => _value;
            internal bool And => _and;

            internal InsertColumnQuery(string columnName)
            {
                _columnName = columnName;
            }

            public void EqualTo(string value, bool and = true)
            {
                _operationName = nameof(EqualTo);
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
