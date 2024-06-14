namespace ARLiteNET.Lib.Common
{
    public interface ISQLQueryBuilder
    {
        ISelectQueryBuilder Select(params string[] columns);
        IInsertQueryBuilder Insert(string table);
    }
}
