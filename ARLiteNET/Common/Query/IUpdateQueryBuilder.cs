
namespace ARLiteNET.Common
{
    public interface IUpdateQueryBuilder : IQueryBuilder
    {
        IUpdateQueryBuilder Set(ColumnValueObject valuesInfo);
        IConditionalFunctionQueryBuilder Where(string column);
    }
}
