namespace ARLiteNET.Common
{
    public interface IOrderByQueryBuilder : IQueryBuilder
    {
        IOrderByQueryBuilder Asc(string column);
        IOrderByQueryBuilder Desc(string column);
    }
}
