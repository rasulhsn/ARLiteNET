using ARLiteNET.Lib.Core;
using ARLiteNET.Lib.SQLite.Tests.Data.InMemory;
using ARLiteNET.Lib.Tests.Data.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [ARLiteConfiguration(typeof(TestInMemorySQLiteConfigurationFactory))]
    public class TestSQLiteObject : ARLiteObject
    {
        public IEnumerable<TestUserObjectDto> GetAll()
        {
            var queryBuilder = this.Query()
                              .SetCommand("SELECT * FROM Users");

            return this.RunEnumerable<TestUserObjectDto>(queryBuilder);
        }

        public void Add(TestUserObjectDto newObject)
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
