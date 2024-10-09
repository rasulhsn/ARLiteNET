﻿
namespace ARLiteNET.SQLite.Integration.Tests.Stub
{
    [ARLiteConfiguration(typeof(InMemorySQLiteConfigurationFactoryStub))]
    public class SQLiteDeclarativeStub : ARLiteObject
    {
        const string TABLE_NAME = "Users";

        public IEnumerable<UserDtoStub> GetAll()
        {
            // Declarative approach
            var selectQuery = base.Query()
                                   .Select<UserDtoStub>(TABLE_NAME);

            selectQuery.Column(x => x.Id).Only();
            selectQuery.Column(x => x.Name).Only();
            selectQuery.Column(x => x.IsActive).Only();

            return base.RunEnumerable<UserDtoStub>(selectQuery);
        }

        public IEnumerable<UserDtoStub> GetByName(string name)
        {
            // Declarative approach
            var selectQuery = this.Query()
                                   .Select<UserDtoStub>(TABLE_NAME);

            selectQuery.Column(x => x.Id).Only();
            selectQuery.Column(x => x.Name).Only();
            selectQuery.Column(x => x.IsActive).Only();
            selectQuery.Column(x => x.Name).EqualTo(name);

            return this.RunEnumerable<UserDtoStub>(selectQuery);
        }

        public bool Add(UserDtoStub newObject)
        {
            // Declarative approach
            var insertQuery = this.Query()
                                    .Insert(TABLE_NAME, newObject);

            insertQuery.Column(x => x.Id).Ignore();
            insertQuery.Column(x => x.BirthDate).Ignore();

            int affectedRows = this.Run(insertQuery);

            return affectedRows == 1;
        }

        public void Update(UserDtoStub newObject)
        {
            // Declarative approach
            var updateQuery = this.Query()
                                    .Update(TABLE_NAME, newObject);

            //updateQuery.Column(nameof(TestUserObject.Id)).EqualTo(newObject.Id);
            //updateQuery.Column(nameof(TestUserObject.Name)).Only();
            //updateQuery.Column(nameof(TestUserObject.IsActive)).Only();

            this.Run(updateQuery);
        }

        public void Delete(int id)
        {
            // Declarative approach
            var deleteQuery = this.Query()
                                    .Delete<UserDtoStub>(TABLE_NAME);

            //deleteQuery.Column(nameof(TestUserObject.Id)).EqualTo(id);

            this.Run(deleteQuery);
        }
    }
}
