namespace ARLiteNET.Common
{
    public interface IInsertQueryBuilder : IQueryBuilder
    {
        IInsertQueryBuilder Value(params InsertValueObject[] valuesInfo);
    }
}
