using ARLiteNET.Common;
using ARLiteNET.SQLite.QueryBuilders;

namespace ARLiteNET.SQLite
{
    public static class SQLiteQueryFactory
    {
        public static ISelectQueryBuilder Select(params string[] columns) => new SQLiteSelectQueryBuilder(columns);

        public static IInsertQueryBuilder Insert(string table) => new SQLiteInsertQueryBuilder(table);

        //public static IUpdateQueryBuilder Update()
        //{
        //    return null;
        //}

        public static IDeleteQueryBuilder Delete(string table) => new SQLiteDeleteQueryBuilder(table);
    }
}
