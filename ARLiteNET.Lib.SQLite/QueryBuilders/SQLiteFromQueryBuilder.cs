using ARLiteNET.Lib.Common;
using System;
using System.Text;

namespace ARLiteNET.Lib.SQLite
{
    public class SQLiteFromQueryBuilder : ChainQueryBuilder, IFromQueryBuilder
    {
        const string FROM = "FROM";
        const string AS = "AS";

        private readonly string _tableName;
        private string _alias;

        private bool HasAlias => !string.IsNullOrWhiteSpace(_alias);
        private string DefaultAlias => HasAlias ? _alias : _tableName;

        public SQLiteFromQueryBuilder(string tableName, ChainQueryBuilder innerQueryBuilder) : base(innerQueryBuilder)
        {
            _tableName = tableName;
        }

        public IFromQueryBuilder Alias(string alias)
        {
            _alias = alias;
            return this;
        }

        public IWhereQueryBuilder Where(string column)
        {
            return new SQLiteWhereQueryBuilder(DefaultAlias, column, this);
        }

        public IOrderByQueryBuilder OrderBy(string column)
        {
            throw new NotImplementedException();
        }

        protected override string Build(QueryBuilderContext? context = null)
        {
            StringBuilder builder = new StringBuilder();

            context = new QueryBuilderContext(DefaultAlias);

            CombineQuery(builder, context);

            if (HasAlias)
            {
                builder.Append($"{FROM} {_tableName} {AS} {_alias} ");
            }
            else
            {
                builder.Append($"{FROM} {_tableName} ");
            }

            return builder.ToString();
        }

        public string Build()
        {
            return Build(null);
        }
    }
}
