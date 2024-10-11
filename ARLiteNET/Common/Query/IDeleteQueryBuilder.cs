
namespace ARLiteNET.Common
{
    public interface IDeleteQueryBuilder : IQueryBuilder
    {
        IDeleteQueryBuilder SetTable(string tableName);
        IConditionalFunctionQueryBuilder Where(string column);
    }
}
