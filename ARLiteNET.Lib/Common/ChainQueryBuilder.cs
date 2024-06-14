using System.Text;

namespace ARLiteNET.Lib.Common
{
    public abstract class ChainQueryBuilder
    {
        protected readonly ChainQueryBuilder queryBuilder;

        public ChainQueryBuilder(ChainQueryBuilder innerQueryBuilder)
        {
            queryBuilder = innerQueryBuilder;
        }

        protected void CombineQuery(StringBuilder builder)
        {
            CombineQuery(builder, null);
        }

        protected void CombineQuery(StringBuilder builder, QueryBuilderContext? context)
        {
            if (queryBuilder != null)
            {
                string nestedQuery = queryBuilder.Build(context);
                builder.Append(nestedQuery);
            }
        }

        protected abstract string Build(QueryBuilderContext? context = null);
    }
}
