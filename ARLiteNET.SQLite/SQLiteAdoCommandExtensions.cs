using ARLiteNET.Common;
using ARLiteNET.Core;
using System;

namespace ARLiteNET.SQLite
{
    public static class SQLiteAdoCommandExtensions
    {
        public static IDbCommandBuilder Object<T>(this
            AdoCommandBuilder builder, Func<ISelectQueryBuilder, IQueryBuilder> setupDelegate)
        {
            var selectQueryBuilder = SQLiteQueryFactory.Select();
            var setupBuilder = setupDelegate(selectQueryBuilder);

            string queryStr = setupBuilder.Build();
            builder.SetCommand(queryStr);

            return builder;
        }

        public static SelectCommandBuilder<T> Select<T>(this
            AdoCommandBuilder builder)
        {
            return builder.Select<T>(nameof(T));
        }

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
