using ARLiteNET.Lib.Integration.Tests.Stub;

namespace ARLiteNET.Lib.SQLite.Integration.Tests.Stub
{
    [ARLiteConfiguration(typeof(InMemorySQLiteConfigurationFactoryStub))]
    public class SQLiteObjectQueryStub : ARLiteObject
    {
        //public IEnumerable<UserDtoStub> GetAll()
        //{
        //    var selectQuery = base.Query()
        //                           .Object<UserDtoStub>((queryBuilder) =>
        //                           {
        //                               queryBuilder.Select()
        //                                           .Where(x => x.Name)
        //                                           .EqualTo("Rasul")
        //                                           .Or(x => x.Id >= 2);
        //                           });

        //    return base.RunEnumerable<UserDtoStub>(selectQuery);
        //}
    }
}
