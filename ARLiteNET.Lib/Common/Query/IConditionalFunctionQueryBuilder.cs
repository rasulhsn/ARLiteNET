
namespace ARLiteNET.Lib.Common.Query
{
    public interface IConditionalFunctionQueryBuilder : IQueryBuilder
    {
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> EqualTo(string value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> In(params string[] values);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> GreaterThan(int value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> GreaterThan(decimal value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> LessThan(int value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> LessThan(decimal value);
    }
}
