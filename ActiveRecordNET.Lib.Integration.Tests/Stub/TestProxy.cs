using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ActiveRecordNET.Lib.Integration.Tests.Helper;

namespace ActiveRecordNET.Lib.Integration.Tests
{
    [AdoConfiguration(typeof(TestAdoConfigurationFactory))]
    public class TestProxy : AdoObjectProxy
    {
        public IEnumerable<TestObject> GetAll()
        {
            return this.ReadRecords<TestObject>((builder) =>
            {
                builder.SetCommand("SELECT * FROM Users");
            });
        }
    }
}
