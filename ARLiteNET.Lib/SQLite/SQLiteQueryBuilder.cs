using ARLiteNET.Lib.Common;

namespace ARLiteNET.Lib
{
    public class SQLiteQueryBuilder : ISQLQueryBuilder
    {
        public IInsertQueryBuilder Insert(string table)
        {
            return new SQLiteInsertQueryBuilder(table);
        }

        public ISelectQueryBuilder Select(params string[] columns)
        {
            return new SQLiteSelectQueryBuilder(columns);
        }
    }
}
