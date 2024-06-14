using ARLiteNET.Lib.Integration.Tests.Helper;
using ARLiteNET.Lib.Integration.Tests.Stub;
using ARLiteNET.Lib.SQLite;
using System.Collections.Generic;

namespace ARLiteNET.Lib.Integration.Tests
{
    [SQLiteConfiguration(typeof(TestSQLiteConfigurationFactory))]
    public class TestSQLiteDeclarativeAdoObject : ARLiteObject
    {
        public IEnumerable<TestUserObject> GetAll()
        {
            // Declarative approach
            var selectQuery = this.Query()
                                   .Select("Users");

            return this.RunEnumerable<TestUserObject>(selectQuery);
        }

        public void Add(TestUserObject newObject)
        {
            // Declarative approach
            var insertQuery = this.Query().Insert("Users", newObject);

            //insertQuery.Column(nameof(TestUserObject.Name)).Only();
            //insertQuery.Column(nameof(TestUserObject.IsActive)).Only();

            this.Run(insertQuery);
        }

        public void Update(TestUserObject newObject)
        {
            // Declarative approach
            var updateQuery = this.Query().Update("Users", newObject);

            //updateQuery.Column(nameof(TestUserObject.Id)).EqualTo(newObject.Id);
            //updateQuery.Column(nameof(TestUserObject.Name)).Only();
            //updateQuery.Column(nameof(TestUserObject.IsActive)).Only();

            this.Run(updateQuery);
        }

        public void Delete(int id)
        {
            // Declarative approach
            var deleteQuery = this.Query().Delete<TestUserObject>("Users");

            //deleteQuery.Column(nameof(TestUserObject.Id)).EqualTo(id);

            this.Run(deleteQuery);
        }
    }
}
