using System;

namespace ARLiteNET.SQLite
{
    /// <summary>
    /// Extensions of declarative approach for SQLite
    /// </summary>
    public static class SQLiteDeclarativeExtensions
    {
        public static SelectCommandBuilder<T> Select<T>(this
            AdoCommandBuilder builder) => builder.Select<T>(nameof(T));

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
            var updateQueryBuilder = SQLiteQueryFactory.Update(tableName);
            return new UpdateCommandBuilder<T>(instance, builder, updateQueryBuilder);
        }

        public static DeleteCommandBuilder<T> Delete<T>(this
           AdoCommandBuilder builder, string tableName)
        {
            var deleteQueryBuilder = SQLiteQueryFactory.Delete(tableName);
            return new DeleteCommandBuilder<T>(builder, deleteQueryBuilder);
        }
    }
}
