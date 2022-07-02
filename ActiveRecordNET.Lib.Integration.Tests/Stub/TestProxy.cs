using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ActiveRecordNET.Integration.Tests.Helper;

namespace ActiveRecordNET.Integration.Tests
{
    public class TestProxy : AdoObjectProxy
    {
        public IEnumerable<TestObject> GetAll()
        {
            return this.ReadRecords<TestObject>((builder) =>
            {
                builder.SetCommand("SELECT * FROM Users");
            });
        }

        protected override void Configure(AdoConnectionStringBuilder builder)
        {
            // For test -> sqllite
            string pathSqlLite = Path.Combine(PathUtils.TryGetRootPath(), "Data", "ARNetDb.db");

            builder.ConnectionString($"Data Source={pathSqlLite};Version=3;")
                   .SQLLite();
        }
    }
}
