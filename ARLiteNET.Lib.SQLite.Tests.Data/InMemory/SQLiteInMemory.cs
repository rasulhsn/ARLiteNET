using System.Data.SQLite;
using System;
using System.Diagnostics;

namespace ARLiteNET.Lib.SQLite.Tests.Data.InMemory
{
    public static class SQLiteInMemory
    {
        public static void PrepareDB()
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
                    
                    if (result == null)
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
                    using (SQLiteCommand createCommand = new SQLiteCommand(SQLiteSettings.DefaultTableScript, connection))
                        createCommand.ExecuteNonQuery();

                    // needs to combine with CREATE TABLE query!
                    using (SQLiteCommand insertDataCommand = new SQLiteCommand(SQLiteSettings.DefaultTableInsertScript, connection))
                        insertDataCommand.ExecuteNonQuery();

                    Debug.WriteLine("Table created successfully.");
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Occur error! {ex.Message}");
                throw ex;
            }
            finally
            {
                connection?.Dispose();
            }
        }
    }
}
