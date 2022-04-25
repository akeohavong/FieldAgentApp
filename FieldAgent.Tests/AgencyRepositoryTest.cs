using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using FieldAgent.DAL.EF;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests
{
    public class AgencyRepositoryTest
    {
        AgencyRepository db;
        DBFactory dbf;
        Agency Fbi = new Agency
        {
            AgencyID = 1,
            ShortName = "FBI",
            LongName = "Federal Bureau of Investigation"
        };

        [SetUp]
        public void Setup()
        {
           dbf = new DBFactory();
           db = new AgencyRepository(dbf);
           dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Response<Agency> actual = db.Get(1);
            Assert.AreEqual(actual.Data.AgencyID, Fbi.AgencyID);
            Assert.AreEqual(actual.Data.LongName, Fbi.LongName);    
        }
        [Test]
        public void TestGetAll()
        {
            Response<List<Agency>> actual = db.GetAll();
            Assert.AreEqual(3, actual.Data.Count());
        }

        [Test]
        public void TestDelete()
        {
            Response actual = db.Delete(1);
            Assert.AreEqual("Agency 1 deleted.", actual.Message);
            Assert.IsTrue(actual.Success);
            using(var db = dbf.GetDbContext())
            {
                Assert.AreEqual(0,db.Agency.Where(a => a.AgencyID == 1).Count());
            }
        }
        [Test]
        public void TestInsert()
        {
            Agency newAgency = new Agency();
            newAgency.ShortName = "TSETSE";
            newAgency.LongName = "Top Secret agEncy of Top SEcrets";

            Response<Agency> actual = db.Insert(newAgency);
            Assert.AreEqual(4, db.GetAll().Data.Count());
            Assert.AreEqual(4, actual.Data.AgencyID);
            Assert.AreEqual("TSETSE", actual.Data.ShortName);
            Assert.AreEqual("Top Secret agEncy of Top SEcrets", actual.Data.LongName);

        }
        [Test]
        public void TestUpdate()
        {
            Fbi.ShortName = "DEA";
            Fbi.LongName = "Drug Enforcement Agency";
            db.Update(Fbi);
            Response<Agency> actual = db.Get(1);
            Assert.AreEqual(Fbi.AgencyID, actual.Data.AgencyID);
            Assert.AreEqual(Fbi.ShortName, actual.Data.ShortName);
            Assert.AreEqual(Fbi.LongName, actual.Data.LongName);
        }

    }
}
