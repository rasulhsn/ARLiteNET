using ARLiteNET.Lib.Common;

namespace ARLiteNET.Lib.SQLite
{
    public static class SQLiteQueryFactory
    {
        public static ISelectQueryBuilder Select(params string[] columns)
        {
            return new SQLiteSelectQueryBuilder(columns);
        }

        public static IInsertQueryBuilder Insert(string table)
        {
            return new SQLiteInsertQueryBuilder(table);
        }

        //public static IUpdateQueryBuilder Update()
        //{
        //    return null;
        //}

        //public static IDeleteQueryBuilder Delete()
        //{
        //    return null;
        //}
    }
}
