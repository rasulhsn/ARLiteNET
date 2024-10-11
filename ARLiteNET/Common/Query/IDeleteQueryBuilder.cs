
namespace ARLiteNET.Common
{
    public interface IDeleteQueryBuilder : IQueryBuilder
    {
        IConditionalFunctionQueryBuilder Where(string column);
    }
}
