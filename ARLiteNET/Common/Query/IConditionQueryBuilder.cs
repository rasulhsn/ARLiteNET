namespace ARLiteNET.Common
{
    public interface IConditionQueryBuilder<TResultInterface> : IQueryBuilder
    {
        TResultInterface And(string column);
        TResultInterface Or(string column);
    }
}
