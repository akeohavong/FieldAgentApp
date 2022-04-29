using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using FieldAgent.DAL.EF;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests
{
    public class AgencyAgentRepositoryTest
    {
        private AgencyAgentRepository db;
        private DBFactory dbf;
        AgencyAgent aa = new AgencyAgent()
        {
            AgencyID = 1,
            AgentID = 1,
            SecurityClearanceID = 6,
            BadgeID = new Guid("dd8a459a-6bbe-4d4f-b834-f8ec65fde081"),
            ActivationDate = new DateTime(1983, 2, 26),
            DeactivationDate = new DateTime(2012, 5, 20),
            IsActive = true,
        };

        [SetUp]
        public void Setup()
        {
            dbf = new DBFactory();
            db = new AgencyAgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Response<AgencyAgent> actual = db.Get(1, 1);
            Assert.IsNotNull(actual);
            Assert.AreEqual(aa.SecurityClearanceID, actual.Data.SecurityClearanceID);
        }

        [Test]
        public void TestInsert()
        {
            AgencyAgent na = new()
            {
                AgencyID = 3,
                AgentID = 9,
                SecurityClearanceID = 9,
                ActivationDate = new DateTime(2020, 2, 26),
                DeactivationDate = new DateTime(2022, 2, 26),
                IsActive = false,
            };
            Response<AgencyAgent> actual = db.Insert(na);
            Assert.IsTrue(actual.Success);
        }
        [Test]
        public void TestDelete()
        {
            Response actual = db.Delete(1, 1);
            Assert.IsTrue(actual.Success);
        }
        [Test]
        public void TestUpdate()
        {
            aa.IsActive = false;
            Response actual = db.Update(aa);
            Assert.IsTrue(actual.Success);
        }
        [Test]
        public void TestGetByAgency()
        {
            Response<List<AgencyAgent>> agency1 = db.GetByAgency(1);
            Assert.AreEqual(2, agency1.Data.Count);
        }
        [Test]
        public void TestGetByAgent()
        {
            Response<List<AgencyAgent>> agent1 = db.GetByAgent(1);
            Assert.AreEqual(2, agent1.Data.Count);
        }
    }
}
