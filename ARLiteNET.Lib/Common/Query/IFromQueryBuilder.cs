namespace ARLiteNET.Lib.Common
{
    public interface IFromQueryBuilder : IQueryBuilder
    {
        IFromQueryBuilder Alias(string alias);
        IWhereQueryBuilder Where(string column);
        IOrderByQueryBuilder OrderBy();
    }
}
