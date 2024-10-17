
namespace ARLiteNET.Common
{
    public interface IConditionalFunctionQueryBuilder : IQueryBuilder
    {
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> EqualTo(string value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> EqualTo(decimal value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> EqualTo(double value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> EqualTo(int value);

        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> NotEqualTo(string value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> NotEqualTo(decimal value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> NotEqualTo(double value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> NotEqualTo(int value);

        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> In(params string[] values);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> In(params decimal[] values);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> In(params double[] values);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> In(params int[] values);

        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> GreaterThan(int value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> GreaterThan(decimal value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> GreaterThan(double value);
        
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> LessThan(int value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> LessThan(decimal value);
        IConditionQueryBuilder<IConditionalFunctionQueryBuilder> LessThan(double value);
    }
}
