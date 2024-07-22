using ARLiteNET.Common;

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
                                                   .From("Users");
                                   });

            return base.RunEnumerable<UserDtoStub>(selectQuery);
        }

        public bool Add(UserDtoStub newObject)
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


            int affectedRows = this.Run(queryBuilder);

            return affectedRows == 1;
        }
    }
}
