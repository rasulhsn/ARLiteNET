using ARLiteNET.Lib.Common;
using ARLiteNET.Lib.SQLite;
using ARLiteNET.Lib.SQLite.Integration.Tests.Stub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteQueryBuilderTests
    {
        [TestMethod]
        public void Sample()
        {
            const string expectedQuery = $"SELECT * FROM {nameof(TestUserObjectDto)} ";

            string generatedQuery = SQLiteQueryFactory.Select()
                                .From(nameof(TestUserObjectDto))
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Sample2()
        {
            const string expectedQuery = $"SELECT * FROM {nameof(TestUserObjectDto)} AS T WHERE T.Name = 'Test' AND T.Id > 0 ";

            string generatedQuery = SQLiteQueryFactory.Select()
                                .From(nameof(TestUserObjectDto))
                                .Alias("T")
                                .Where(nameof(TestUserObjectDto.Name))
                                .EqualTo("Test")
                                .And(nameof(TestUserObjectDto.Id))
                                .GreaterThan(0)
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Sample3()
        {
            const string expectedQuery = $"SELECT T.Id,T.Name FROM {nameof(TestUserObjectDto)} AS T WHERE T.Name = 'Test' AND T.Id > 0 ";

            string generatedQuery = SQLiteQueryFactory.Select("Id","Name")
                                .From(nameof(TestUserObjectDto))
                                .Alias("T")
                                .Where(nameof(TestUserObjectDto.Name))
                                .EqualTo("Test")
                                .And(nameof(TestUserObjectDto.Id))
                                .GreaterThan(0)
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Sample4()
        {
            const string nameRaw = "Rasul"; const string isActiveRaw = "1";
            const string expectedQuery = $"INSERT INTO {nameof(TestUserObjectDto)} ({nameof(TestUserObjectDto.Name)},{nameof(TestUserObjectDto.IsActive)}) VALUES ('{nameRaw}',{isActiveRaw}), ('{nameRaw}',{isActiveRaw})";

            string generatedQuery = SQLiteQueryFactory.Insert(nameof(TestUserObjectDto))
                                                .Value(new InsertValueObject(nameof(TestUserObjectDto.Name), nameRaw, InsertDataType.TEXT)
                                                       ,new InsertValueObject(nameof(TestUserObjectDto.IsActive), isActiveRaw, InsertDataType.INTEGER))
                                                .Value(new InsertValueObject(nameof(TestUserObjectDto.Name), nameRaw, InsertDataType.TEXT)
                                                       ,new InsertValueObject(nameof(TestUserObjectDto.IsActive), isActiveRaw, InsertDataType.INTEGER))
                                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }
    }
}
