namespace ARLiteNET.Common
{
    public interface IWhereQueryBuilder : IQueryBuilder
    {
        IOrderByQueryBuilder OrderBy();

        IConditionQueryBuilder<IWhereQueryBuilder> EqualTo(string value);
        IConditionQueryBuilder<IWhereQueryBuilder> EqualTo(double value);
        IConditionQueryBuilder<IWhereQueryBuilder> EqualTo(int value);

        IConditionQueryBuilder<IWhereQueryBuilder> NotEqualTo(string value);
        IConditionQueryBuilder<IWhereQueryBuilder> NotEqualTo(double value);
        IConditionQueryBuilder<IWhereQueryBuilder> NotEqualTo(int value);


        IConditionQueryBuilder<IWhereQueryBuilder> In(params string[] values);
        IConditionQueryBuilder<IWhereQueryBuilder> In(params double[] values);
        IConditionQueryBuilder<IWhereQueryBuilder> In(params int[] values);

        IConditionQueryBuilder<IWhereQueryBuilder> GreaterThan(int value);
        IConditionQueryBuilder<IWhereQueryBuilder> GreaterThan(double value);

        IConditionQueryBuilder<IWhereQueryBuilder> LessThan(int value);
        IConditionQueryBuilder<IWhereQueryBuilder> LessThan(decimal value);
    }
}
