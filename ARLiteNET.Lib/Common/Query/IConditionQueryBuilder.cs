namespace ARLiteNET.Lib.Common
{
    public interface IConditionQueryBuilder : IQueryBuilder
    {
        IWhereQueryBuilder And(string column);
        IWhereQueryBuilder Or(string column);
    }
}
