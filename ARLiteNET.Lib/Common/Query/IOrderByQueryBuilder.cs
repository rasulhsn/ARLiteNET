namespace ARLiteNET.Lib.Common
{
    public interface IOrderByQueryBuilder : IQueryBuilder
    {
        IQueryBuilder Asc();
        IQueryBuilder Desc();
    }
}
