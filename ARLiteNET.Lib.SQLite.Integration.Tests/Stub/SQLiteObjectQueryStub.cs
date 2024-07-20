using ARLiteNET.Lib.Integration.Tests.Stub;

namespace ARLiteNET.Lib.SQLite.Integration.Tests.Stub
{
    [ARLiteConfiguration(typeof(InMemorySQLiteConfigurationFactoryStub))]
    public class SQLiteObjectQueryStub : ARLiteObject
    {
        public IEnumerable<UserDtoStub> GetAll()
        {
            var selectQuery = base.Query()
                                   .Object<UserDtoStub>((queryBuilder) =>
                                   {
                                       return queryBuilder.Select()
                                                   .From("Users")
                                                   .Where(nameof(UserDtoStub.Name))
                                                   .EqualTo("Rasul")
                                                   .Or(nameof(UserDtoStub.Id))
                                                   .GreaterThan(2);
                                   });

            return base.RunEnumerable<UserDtoStub>(selectQuery);
        }
    }
}
