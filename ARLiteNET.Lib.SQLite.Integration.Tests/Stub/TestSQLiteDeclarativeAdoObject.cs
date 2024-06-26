using ARLiteNET.Lib.Core;
using ARLiteNET.Lib.SQLite;
using ARLiteNET.Lib.Tests.Data.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [ARLiteConfiguration(typeof(TestInMemorySQLiteConfigurationFactory))]
    public class TestSQLiteDeclarativeAdoObject : ARLiteObject
    {
        public IEnumerable<TestUserObjectDto> GetAll()
        {
            // Declarative approach
            var selectQuery = base.Query()
                                   .Select<TestUserObjectDto>("Users");

            selectQuery.Column(x => x.Id).Only();
            selectQuery.Column(x => x.Name).Only();
            selectQuery.Column(x => x.IsActive).Only();

            return base.RunEnumerable<TestUserObjectDto>(selectQuery);
        }

        public IEnumerable<TestUserObjectDto> GetByName(string name)
        {
            // Declarative approach
            var selectQuery = this.Query()
                                   .Select<TestUserObjectDto>("Users");

            selectQuery.Column(x => x.Id).Only();
            selectQuery.Column(x => x.Name).Only();
            selectQuery.Column(x => x.IsActive).Only();
            selectQuery.Column(x => x.Name).EqualTo(name);

            return this.RunEnumerable<TestUserObjectDto>(selectQuery);
        }

        public void Add(TestUserObjectDto newObject)
        {
            // Declarative approach
            var insertQuery = this.Query()
                                    .Insert("Users", newObject);

            //insertQuery.Column(x => x.IsActive).Ignore();

            this.Run(insertQuery);
        }

        public void Update(TestUserObjectDto newObject)
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
                                    .Delete<TestUserObjectDto>("Users");

            //deleteQuery.Column(nameof(TestUserObject.Id)).EqualTo(id);

            this.Run(deleteQuery);
        }
    }
}
