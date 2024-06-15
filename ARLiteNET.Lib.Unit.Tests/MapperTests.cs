using ARLiteNET.Lib.Core.Mappers;
using ARLiteNET.Lib.Unit.Tests.Stub;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ARLiteNET.Lib.Unit.Tests
{
    [TestClass]
    public class MapperTests
    {
        [TestMethod]
        public void Map_ValidMapped_NotNull()
        {
            TestUserObjectDto instance = new TestUserObjectDto() { Id = 1, Name = "Rasul", IsActive = true, BirthDate = DateTime.Now };

            MapType mapTypeInstance = Mapper.Map(instance);

            Assert.IsNotNull(mapTypeInstance);
            Assert.IsTrue(mapTypeInstance.HasMembers);
        }

        [TestMethod]
        public void Map_ValidMembersCount_Mapped4PropertiesAsMember()
        {
            TestUserObjectDto instance = new TestUserObjectDto() { Id = 1, Name = "Rasul", IsActive = true, BirthDate = DateTime.Now };

            MapType mapTypeInstance = Mapper.Map(instance);

            Assert.IsTrue(mapTypeInstance.Members.Count() == 4);
        }
    }
}