
namespace ARLiteNET.Integration.Tests.Data
{
    public class SQLiteSettings
    {
        public static string DatabaseName { get; } = "InMemoryARLiteNET";
        public static string DefaultTableName { get; } = "Users";
        public static string ConnectionStringVersion3 { get; } = $"Data Source={DatabaseName};Mode=Memory;Cache=Shared;Version=3;";
        
        public static string GetDefaultTableScript() => $"CREATE TABLE \"{DefaultTableName}\" (\r\n\t\"{nameof(SQLiteDefaultTableObject.Id)}\"\tINTEGER NOT NULL,\r\n\t\"{nameof(SQLiteDefaultTableObject.Name)}\"\tTEXT,\r\n\t\"{nameof(SQLiteDefaultTableObject.IsActive)}\"\tINTEGER NOT NULL,\r\n\tPRIMARY KEY(\"Id\")\r\n)";
        public static string GetDefaultTableInsertScript() => $"INSERT INTO {DefaultTableName} ({nameof(SQLiteDefaultTableObject.Id)},{nameof(SQLiteDefaultTableObject.Name)},{nameof(SQLiteDefaultTableObject.IsActive)}) VALUES (1, 'Rasul', 1), (2, 'Huseynov', 1)";
    }
}
