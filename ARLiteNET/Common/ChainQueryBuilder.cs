using System.Text;

namespace ARLiteNET.Common
{
    public abstract class ChainQueryBuilder
    {    
        protected readonly ChainQueryBuilder QueryBuilder;
        protected bool HasNestedQueryBuilder => QueryBuilder != null;

        public ChainQueryBuilder(ChainQueryBuilder innerQueryBuilder) => QueryBuilder = innerQueryBuilder;

        protected void BuildChain(StringBuilder builder)
        {
            BuildChain(builder, null);
        }

        protected void BuildChain(StringBuilder builder, QueryBuilderContext? context)
        {
            if (HasNestedQueryBuilder)
            {
                string nestedQuery = QueryBuilder.Build(context);
                builder.Append(nestedQuery);
            }
        }

        protected abstract string Build(QueryBuilderContext? context = null);
    }
}
