
namespace ARLiteNET.Lib.Common.Query
{
    public interface IDeleteQueryBuilder : IQueryBuilder
    {
        IConditionalFunctionQueryBuilder Where(string column);
    }
}
