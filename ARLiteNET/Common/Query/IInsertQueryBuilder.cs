namespace ARLiteNET.Common
{
    public interface IInsertQueryBuilder : IQueryBuilder
    {
        IInsertQueryBuilder Value(params ColumnValueObject[] valuesInfo);
    }
}
