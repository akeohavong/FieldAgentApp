using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using FieldAgent.DAL.EF;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests
{
    public class AgentRepositoryTest
    {
        AgentRepository db;
        DBFactory dbf;
        Agent agent = new Agent()
        {
            AgentID = 1,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1980, 1, 2),
            Height = 70M
        };
        

        [SetUp]
        public void Setup()
        {
            dbf = new DBFactory();
            db = new AgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Response<Agent> actual = db.Get(1);
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Data.AgentID, agent.AgentID);
            Assert.AreEqual(actual.Data.FirstName, agent.FirstName);
            Assert.AreEqual(actual.Data.LastName, agent.LastName);
            Assert.AreEqual(actual.Data.DateOfBirth, agent.DateOfBirth);
            Assert.AreEqual(actual.Data.Height, agent.Height);
        }

        [Test]
        public void TestInsert()
        {
            Agent newAgent = new Agent()
            {
                FirstName = "Lebron",
                LastName = "James",
                DateOfBirth = new DateTime(1984, 11, 1),
                Height = 90
            };
            Response<Agent> actual = db.Insert(newAgent);
            using(var db = dbf.GetDbContext())
            {
                int numOfAgents = db.Agent.ToList().Count();
                Assert.AreEqual(11, numOfAgents);
            }
            Assert.AreEqual(11, actual.Data.AgentID);
            Assert.AreEqual("Lebron", actual.Data.FirstName);
            Assert.AreEqual("James", actual.Data.LastName);
        }

        [Test]
        public void TestUpdate()
        {
            agent.FirstName = "Bob";
            agent.LastName = "Jones";
            db.Update(agent);
            Assert.AreNotEqual("John", agent.FirstName);
            Assert.AreNotEqual("Doe", agent.LastName);
            Assert.AreEqual(1, agent.AgentID);
            Assert.AreEqual("Bob", agent.FirstName);
            Assert.AreEqual("Jones", agent.LastName);
        }

        [Test]
        public void TestDelete()
        {
            Response actual = db.Delete(1);
            Console.WriteLine(actual.Message);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Agent 1 deleted.", actual.Message);
        }

        [Test]
        public void TestGetMissions()
        {
            Response<List<Mission>> agent1 = db.GetMissions(1);
            Assert.IsTrue(agent1.Success);
            Assert.AreEqual(1, agent1.Data.Count);
            Response<List<Mission>> agent3 = db.GetMissions(3);
            Assert.IsTrue(agent3.Success);
            Assert.AreEqual(2, agent3.Data.Count);
        }

    }
}
