using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using FieldAgent.DAL.EF;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests
{
    public class MissionRepositoryTest
    {
        private MissionRepository db;
        private DBFactory dbf;
        Mission m = new Mission()
        {
            MissionID = 1,
            AgencyID = 1,
            CodeName = "Mat Lam Tam",
            StartDate = new DateTime(2006, 9, 1),
            ProjectedEndDate = new DateTime(2021, 5, 28),
            ActualEndDate = new DateTime(2006, 6, 23),
            OperationalCost = 7919.77M,
            Notes = "Crime|Drama|Thriller|War"

        };

        [SetUp]
        public void Setup()
        {
            dbf = new DBFactory();
            db = new MissionRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Response<Mission> actual = db.Get(1);
            Assert.IsNotNull(actual);
            Assert.AreEqual(m.CodeName, actual.Data.CodeName);
        }

        [Test]
        public void TestInsert()
        {
            Mission newMission = new Mission()
            {
                AgencyID = 3,
                CodeName = "Zero",
                StartDate = new DateTime(2007, 9, 1),
                ProjectedEndDate = new DateTime(2021, 7, 28),
                ActualEndDate = new DateTime(2021, 7, 23),
                OperationalCost = 10000M,
                Notes = "Crime|Drama|Thriller|War"
            };

            Response<Mission> actual = db.Insert(newMission);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Mission 11 added.", actual.Message);
            Assert.AreEqual("Zero", actual.Data.CodeName);
        }

        [Test]
        public void TestUpdate()
        {
            m.Notes = "Updated Notes";
            m.CodeName = "New codename";
            db.Update(m);
            Response<Mission> actual = db.Get(1);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(m.Notes, actual.Data.Notes);
            Assert.AreEqual(m.CodeName, actual.Data.CodeName);
            Assert.AreEqual(m.StartDate, actual.Data.StartDate);

        }
        [Test]
        public void TestDelete()
        {
            Response actual = db.Delete(1);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Mission 1 deleted.", actual.Message);
        }
        [Test]
        public void TestGetByAgency()
        {
            Response<List<Mission>> agency1 = db.GetByAgency(1);
            Assert.AreEqual(3, agency1.Data.Count);
            Response<List<Mission>> agency2 = db.GetByAgency(2);
            Assert.AreEqual(3, agency2.Data.Count);
            Response<List<Mission>> agency3 = db.GetByAgency(3);
            Assert.AreEqual(4, agency3.Data.Count);
        }
        [Test]
        public void TestGetByAgent()
        {
            Response<List<Mission>> agent1 = db.GetByAgent(1);
            Assert.IsTrue(agent1.Success);
            Assert.AreEqual(1, agent1.Data.Count);
            Response<List<Mission>> agent3 = db.GetByAgent(3);
            Assert.IsTrue(agent3.Success);
            Assert.AreEqual(2, agent3.Data.Count);
        }
    }
}