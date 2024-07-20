using ARLiteNET.Lib.Common;
using System;

namespace ARLiteNET.Lib.SQLite.QueryBuilders
{
    public class SQLiteOrderByQueryBuilder : ChainQueryBuilder,
                                                IOrderByQueryBuilder
    {
        private readonly string _alias;
        private bool _hasAlias => !string.IsNullOrEmpty(_alias);

        public SQLiteOrderByQueryBuilder(string alias, ChainQueryBuilder innerQueryBuilder) : base(innerQueryBuilder)
        {
            _alias = alias;
        }

        public IOrderByQueryBuilder Asc(string column)
        {
            throw new NotImplementedException();
        }

        public IOrderByQueryBuilder Desc(string column)
        {
            throw new NotImplementedException();
        }

        protected override string Build(QueryBuilderContext? context = null)
        {
            throw new NotImplementedException();
        }

        public string Build()
        {
            throw new NotImplementedException();
        }
    }
}
