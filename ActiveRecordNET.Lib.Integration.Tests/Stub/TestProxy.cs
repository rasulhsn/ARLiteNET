using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ActiveRecordNET.Integration.Tests.Helper;

namespace ActiveRecordNET.Integration.Tests
{
    public class TestProxy : AdoObjectProxy
    {
        private readonly AdoConnectionStringBuilder _connectionStringBuilder;

        public TestProxy()
        {
            // For test -> sqllite
            string pathSqlLite = Path.Combine(PathUtils.TryGetRootPath(), "Data", "ARNetDb.db");
            
            _connectionStringBuilder = new AdoConnectionStringBuilder()
                        .ConnectionString($"Data Source={pathSqlLite};Version=3;")
                        .SQLLite();
        }

        public IEnumerable<TestObject> GetAll()
        {
            return this.ReadRecords<TestObject>(() =>
            {
                return _connectionStringBuilder
                        .CreateCommand()
                        .SetCommand("SELECT * FROM Users");
            });
        }
    }
}
