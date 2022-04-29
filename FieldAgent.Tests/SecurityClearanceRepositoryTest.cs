using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using FieldAgent.DAL.EF;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests
{
    public class SecurityClearanceRepositoryTest
    {
        private SecurityClearanceRepository db;
        DBFactory dbf;
        SecurityClearance sc = new SecurityClearance()
        {
            SecurityClearanceID = 6,
            SecurityClearanceName = "Editor"
        };
        
        [SetUp]
        public void Setup()
        {
            dbf = new DBFactory();
            db = new SecurityClearanceRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Response<SecurityClearance> actual = db.Get(6);
            Assert.IsNotNull(actual);
            Assert.AreEqual(sc.SecurityClearanceName, actual.Data.SecurityClearanceName);
        }
        [Test]
        public void TestGetAll()
        {
            Response<List<SecurityClearance>> actual = db.GetAll();
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(10, actual.Data.Count);
        }
    }
}
