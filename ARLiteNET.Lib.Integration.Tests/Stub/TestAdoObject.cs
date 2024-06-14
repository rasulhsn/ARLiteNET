using ARLiteNET.Lib.Integration.Tests.Helper;
using ARLiteNET.Lib.Integration.Tests.Stub;
using ARLiteNET.Lib.SQLite;
using System.Collections.Generic;

namespace ARLiteNET.Lib.Integration.Tests
{
    [SQLiteConfiguration(typeof(TestSQLLiteConfigurationFactory))]
    public class TestAdoObject : BaseARLiteObject
    {
        public IEnumerable<TestUserObject> GetAll()
        {
            var queryBuilder = this.Query()
                              .SetCommand("SELECT * FROM Users");

            return this.RunEnumerable<TestUserObject>(queryBuilder);
        }

        public void Add(TestUserObject newObject)
        {
            var queryBuilder = this.Query()
                .SetCommand("INSERT INTO Users (Name, IsActive) VALUES (@name, @isActive)")
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

            this.Run(queryBuilder);
        }
    }
}
