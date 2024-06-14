using ARLiteNET.Lib.Core;

namespace ARLiteNET.Lib
{
    public static class AdoCommandExtensions
    {
        public static SelectCommandBuilder Select(this
            AdoCommandBuilder builder, string tableName)
        {
            var selectQueryBuilder = new SQLiteQueryBuilder().Select();

            return new SelectCommandBuilder(tableName, builder, selectQueryBuilder);
        }

        public static InsertCommandBuilder<T> Insert<T>(this
           AdoCommandBuilder builder, string sourceName, T instance)
        {
            return null;
        }

        public static UpdateCommandBuilder<T> Update<T>(this
           AdoCommandBuilder builder, string sourceName, T instance)
        {
            return null;
        }

        public static DeleteCommandBuilder<T> Delete<T>(this
           AdoCommandBuilder builder, string sourceName)
        {
            return null;
        }
    }
}
