using System.Data.SQLite;
using System.Diagnostics;

namespace ARLiteNET.Integration.Tests.Data
{
    public static class SQLiteInMemory
    {
        public static void PrepareDBForTest()
        {
            SQLiteConnection connection = null;

            try
            {
                string checkTableQuery = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{SQLiteSettings.DefaultTableName}';";
                bool checkTableExists = false;

                connection = new SQLiteConnection(SQLiteSettings.ConnectionStringVersion3);

                connection.Open();

                using (SQLiteCommand checkCommand = new SQLiteCommand(checkTableQuery, connection))
                {
                    var result = checkCommand.ExecuteScalar();
                    
                    if (result is null)
                    {
                        checkTableExists = false;
                    }
                    else
                    {
                        checkTableExists = true;
                    }
                }

                if (!checkTableExists)
                {
                    using (SQLiteCommand createCommand = new SQLiteCommand(SQLiteSettings.GetDefaultTableScript(), connection))
                        createCommand.ExecuteNonQuery();

                    // needs to combine with CREATE TABLE query!
                    using (SQLiteCommand insertDataCommand = new SQLiteCommand(SQLiteSettings.GetDefaultTableInsertScript(), connection))
                        insertDataCommand.ExecuteNonQuery();

                    Debug.WriteLine("Table created successfully with data.");
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Occur error! {ex.Message}");
                throw;
            }
            finally
            {
                connection?.Dispose();
            }
        }
    }
}
