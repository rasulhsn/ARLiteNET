using ARLiteNET.Lib.Integration.Tests.Stub;
using ARLiteNET.Lib.SQLite;

namespace ARLiteNET.Lib.Integration.Tests
{
    [ARLiteConfiguration(typeof(InMemorySQLiteConfigurationFactoryStub))]
    public class SQLiteDeclarativeAdoObjectStub : ARLiteObject
    {
        public IEnumerable<UserObjectDtoStub> GetAll()
        {
            // Declarative approach
            var selectQuery = base.Query()
                                   .Select<UserObjectDtoStub>("Users");

            selectQuery.Column(x => x.Id).Only();
            selectQuery.Column(x => x.Name).Only();
            selectQuery.Column(x => x.IsActive).Only();

            return base.RunEnumerable<UserObjectDtoStub>(selectQuery);
        }

        public IEnumerable<UserObjectDtoStub> GetByName(string name)
        {
            // Declarative approach
            var selectQuery = this.Query()
                                   .Select<UserObjectDtoStub>("Users");

            selectQuery.Column(x => x.Id).Only();
            selectQuery.Column(x => x.Name).Only();
            selectQuery.Column(x => x.IsActive).Only();
            selectQuery.Column(x => x.Name).EqualTo(name);

            return this.RunEnumerable<UserObjectDtoStub>(selectQuery);
        }

        public bool Add(UserObjectDtoStub newObject)
        {
            // Declarative approach
            var insertQuery = this.Query()
                                    .Insert("Users", newObject);

            insertQuery.Column(x => x.Id).Ignore();
            insertQuery.Column(x => x.BirthDate).Ignore();

            int affectedRows = this.Run(insertQuery);

            return affectedRows == 1;
        }

        public void Update(UserObjectDtoStub newObject)
        {
            // Declarative approach
            var updateQuery = this.Query()
                                    .Update("Users", newObject);

            //updateQuery.Column(nameof(TestUserObject.Id)).EqualTo(newObject.Id);
            //updateQuery.Column(nameof(TestUserObject.Name)).Only();
            //updateQuery.Column(nameof(TestUserObject.IsActive)).Only();

            this.Run(updateQuery);
        }

        public void Delete(int id)
        {
            // Declarative approach
            var deleteQuery = this.Query()
                                    .Delete<UserObjectDtoStub>("Users");

            //deleteQuery.Column(nameof(TestUserObject.Id)).EqualTo(id);

            this.Run(deleteQuery);
        }
    }
}
