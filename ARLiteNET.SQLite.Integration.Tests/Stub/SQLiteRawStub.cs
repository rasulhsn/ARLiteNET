
namespace ARLiteNET.SQLite.Integration.Tests.Stub
{
    [ARLiteConfiguration(typeof(InMemorySQLiteConfigurationFactoryStub))]
    public class SQLiteRawStub : ARLiteObject
    {
        public IEnumerable<UserDtoStub> GetAll()
        {
            var queryBuilder = this.Query()
                              .SetCommand("SELECT * FROM Users");

            return this.RunEnumerable<UserDtoStub>(queryBuilder);
        }

        public bool Add(UserDtoStub newObject)
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

            int affectedRows = this.Run(queryBuilder);

            return affectedRows == 1;
        }
    }
}
