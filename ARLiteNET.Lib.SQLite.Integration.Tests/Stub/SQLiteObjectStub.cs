using ARLiteNET.Lib.Integration.Tests.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [ARLiteConfiguration(typeof(InMemorySQLiteConfigurationFactoryStub))]
    public class SQLiteObjectStub : ARLiteObject
    {
        public IEnumerable<UserObjectDtoStub> GetAll()
        {
            var queryBuilder = this.Query()
                              .SetCommand("SELECT * FROM Users");

            return this.RunEnumerable<UserObjectDtoStub>(queryBuilder);
        }

        public void Add(UserObjectDtoStub newObject)
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
