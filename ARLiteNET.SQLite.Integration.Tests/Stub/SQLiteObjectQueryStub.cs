using ARLiteNET.Common;
using ARLiteNET.Integration.Tests.Stub;

namespace ARLiteNET.SQLite.Integration.Tests.Stub
{
    [ARLiteConfiguration(typeof(InMemorySQLiteConfigurationFactoryStub))]
    public class SQLiteObjectQueryStub : ARLiteObject
    {
        public IEnumerable<UserDtoStub> GetAll()
        {
            var selectQuery = base.Query()
                                   .ObjectSelect<UserDtoStub>((queryBuilder) =>
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

        public void Add(UserDtoStub newObject)
        {
            var queryBuilder = this.Query()
                                     .ObjectInsert<UserDtoStub>("Users", (queryBuilder) =>
                                     {
                                         InsertValueObject[] insertValue =
                                         [
                                             new(nameof(newObject.Name), newObject.Name, InsertDataType.TEXT),
                                             new(nameof(newObject.IsActive), newObject.IsActive, InsertDataType.BOOLEAN)
                                         ];
                                         return queryBuilder.Value(insertValue);     
                                     });

            this.Run(queryBuilder);
        }
    }
}
