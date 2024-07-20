namespace ARLiteNET.Lib.Common
{
    public interface IWhereQueryBuilder : IQueryBuilder
    {
        IConditionQueryBuilder<IWhereQueryBuilder> EqualTo(string value);
        IConditionQueryBuilder<IWhereQueryBuilder> In(params string[] values);
        IConditionQueryBuilder<IWhereQueryBuilder> GreaterThan(int value);
        IConditionQueryBuilder<IWhereQueryBuilder> GreaterThan(decimal value);
        IConditionQueryBuilder<IWhereQueryBuilder> LessThan(int value);
        IConditionQueryBuilder<IWhereQueryBuilder> LessThan(decimal value);
        IOrderByQueryBuilder OrderBy();
    }
}
