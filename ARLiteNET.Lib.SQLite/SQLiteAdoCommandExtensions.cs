using ARLiteNET.Lib.Core;
using System;

namespace ARLiteNET.Lib.SQLite
{
    public static class SQLiteAdoCommandExtensions
    {
        public static SelectCommandBuilder<T> Select<T>(this
            AdoCommandBuilder builder, string tableName)
        {
            var selectQueryBuilder = SQLiteQueryFactory.Select();
            return new SelectCommandBuilder<T>(tableName, builder, selectQueryBuilder);
        }

        public static InsertCommandBuilder<T> Insert<T>(this
           AdoCommandBuilder builder, string tableName, T instance)
        {
            var insertQueryBuilder = SQLiteQueryFactory.Insert(tableName);
            return new InsertCommandBuilder<T>(instance, builder, insertQueryBuilder);
        }

        public static UpdateCommandBuilder<T> Update<T>(this
           AdoCommandBuilder builder, string tableName, T instance)
        {
            throw new NotImplementedException();
        }

        public static DeleteCommandBuilder<T> Delete<T>(this
           AdoCommandBuilder builder, string tableName)
        {
            throw new NotImplementedException();
        }
    }
}
