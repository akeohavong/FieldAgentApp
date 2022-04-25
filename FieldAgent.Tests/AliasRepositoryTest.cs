using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using FieldAgent.DAL.EF;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests
{
    public class AliasRepositoryTest
    {
        AliasRepsitory db;
        DBFactory dbf;
        Alias a = new Alias()
        {
            AliasID = 1,
            AgentID = 10,
            AliasName = "Pelican, brown",
            InterpolID = new Guid("2ef84bee-3cb8-4a25-ab03-7d6e061ead9f"),
            Persona = "benchmark leading-edge methodologies"

        };

        [SetUp]
        public void Setup()
        {
            dbf = new DBFactory();
            db = new AliasRepsitory(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }
        [Test]
        public void TestGet()
        {
            Response<Alias> actual = db.Get(1);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void TestDelete()
        {
            Response actual = db.Delete(1);
            Assert.AreEqual("Alias 1 deleted.", actual.Message);
            Assert.IsTrue(actual.Success);

        }

        [Test]
        public void TestGetByAgent()
        {
            Response<List<Alias>> actual = db.GetByAgent(1);
            Assert.AreEqual(3, actual.Data.Count);
        }

        [Test]
        public void TestInsert()
        {
            Alias newAlias = new()
            {
                AgentID = 7,
                AliasName = "007",
                InterpolID = Guid.NewGuid(),
                Persona = "new persona"
            };

            Response<Alias> actual = db.Insert(newAlias);
            Assert.AreEqual("Alias 11 added.", actual.Message);
            Assert.IsTrue(actual.Success);
        }

        [Test]
        public void TestUpdate()
        {
            a.AgentID = 7;
            a.AliasName = "Darth Vader";
            a.InterpolID = new Guid();
            db.Update(a);
            Response actual = db.Get(1);
            Assert.AreNotEqual("Pelican, brown", a.AliasName);
            Assert.AreEqual("Darth Vader", a.AliasName);
            Assert.IsTrue(actual.Success); 
            

        }

    }
}
