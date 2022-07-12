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
            return this.RunEnumerable<TestObject>((builder) =>
            {
                builder.SetCommand("SELECT * FROM Users");
            });
        }

        public void Add(TestObject newObject)
        {
            this.Run((builder) =>
            {
                builder.SetCommand("INSERT INTO Users (Name, IsActive) VALUES (@name, @isActive)")
                    .AddParam((param) =>
                    {
                        param.ParameterName = "@name";
                        param.DbType = System.Data.DbType.String;
                        param.Value = newObject.Name;
                    }).AddParam((param) => {
                        param.ParameterName = "@isActive";
                        //param.DbType = System.Data.DbType.Boolean;
                        param.Value = newObject.IsActive;
                    });
            });
        }
    }
}
