namespace ARLiteNET.Lib.Common
{
    public interface IWhereQueryBuilder : IQueryBuilder
    {
        IConditionQueryBuilder EqualTo(string value);
        IConditionQueryBuilder In(params string[] values);
        IConditionQueryBuilder GreaterThan(int value);
        IConditionQueryBuilder GreaterThan(decimal value);
        IConditionQueryBuilder LessThan(int value);
        IConditionQueryBuilder LessThan(decimal value);

        IOrderByQueryBuilder OrderBy(string columnName);
    }
}
