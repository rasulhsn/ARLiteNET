using ARLiteNET.Common;
using System;

namespace ARLiteNET.SQLite
{
    /// <summary>
    /// Extensions of query object approach for SQLite
    /// </summary>
    public static class SQLiteQueryObjectExtensions
    {
        public static IDbCommandBuilder ObjectSelect<T>(this
            AdoCommandBuilder builder, Func<ISelectQueryBuilder, IQueryBuilder> setupDelegate)
        {
            var selectQueryBuilder = SQLiteQueryFactory.Select();
            var setupBuilder = setupDelegate(selectQueryBuilder);

            string queryStr = setupBuilder.Build();
            builder.SetCommand(queryStr);

            return builder;
        }

        public static IDbCommandBuilder ObjectInsert<T>(this
            AdoCommandBuilder builder, string tableName, Func<IInsertQueryBuilder, IQueryBuilder> setupDelegate)
        {
            var insertQueryBuilder = SQLiteQueryFactory.Insert(tableName);
            var setupBuilder = setupDelegate(insertQueryBuilder);

            string queryStr = setupBuilder.Build();
            builder.SetCommand(queryStr);

            return builder;
        }
    }
}
