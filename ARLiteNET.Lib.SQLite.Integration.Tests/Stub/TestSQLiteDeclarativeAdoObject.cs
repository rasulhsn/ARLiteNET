using ARLiteNET.Lib.Core;
using ARLiteNET.Lib.Integration.Tests.Helper;
using ARLiteNET.Lib.Integration.Tests.Stub;
using System.Collections.Generic;
using ARLiteNET.Lib.SQLite;

namespace ARLiteNET.Lib.Integration.Tests
{
    [ARLiteConfiguration(typeof(TestSQLiteConfigurationFactory))]
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
            var insertQuery = this.Query()
                                    .Insert("Users", newObject);

            insertQuery.Column(x => x.IsActive).Ignore();

            this.Run(insertQuery);
        }

        public void Update(TestUserObject newObject)
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
                                    .Delete<TestUserObject>("Users");

            //deleteQuery.Column(nameof(TestUserObject.Id)).EqualTo(id);

            this.Run(deleteQuery);
        }
    }
}
