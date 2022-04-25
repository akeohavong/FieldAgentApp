using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using FieldAgent.DAL.EF;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests
{
    public class LocationRepositoryTest
    {
        LocationRepository db;
        DBFactory dbf;
        Location loc = new Location()
        {
            AgencyID = 3,
            LocationName = "King, D''Amore and Auer",
            Street1 = "Basil",
            Street2 = "Marcy",
            City = "Kuala Belait",
            PostalCode = "55555",
            CountryCode = "BN"
        };

        [SetUp]
        public void Setup()
        {
            dbf = new DBFactory();
            db = new LocationRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }
        [Test]
        public void TestGet()
        {
            Response<Location> actual = db.Get(1);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void TestDelete()
        {
            Response actual = db.Delete(1);
            Assert.AreEqual("Location 1 deleted.", actual.Message);
            Assert.IsTrue(actual.Success);

        }

        [Test]
        public void TestGetByAgency()
        {
            Response<List<Location>> agency1 = db.GetByAgency(1);  
            Assert.AreEqual(4, agency1.Data.Count);
            Response<List<Location>> agency2 = db.GetByAgency(2);
            Assert.AreEqual(5, agency2.Data.Count);
            Response<List<Location>> agency3 = db.GetByAgency(3);
            Assert.AreEqual(1, agency3.Data.Count);
        }

        [Test]
        public void TestInsert()
        {
            Location newLocation = new Location()
            {
                AgencyID = 3,
                LocationName = "Secret Base",
                Street1 = "55555",
                Street2 = "Main Street",
                City = "Gotham City",
                PostalCode = "55555",
                CountryCode = "US"
            };
            Response<Location> actual = db.Insert(newLocation);
            Assert.AreEqual(2, db.GetByAgency(3).Data.Count);
            Assert.AreEqual("Location 11 added.", actual.Message);
            Assert.IsTrue(actual.Success);
        }

        [Test]
        public void TestUpdate()
        {

            loc.LocationName = "Secret Base";
            loc.Street1 = "55555";
            loc.Street2 = "Main Street";
            loc.City = "Gotham City";
            loc.PostalCode = "55555";
            loc.CountryCode = "US";
            db.Update(loc);
            Response actual = db.Get(1);
            Assert.AreNotEqual("King, D''Amore and Auer", loc.LocationName);
            Assert.AreEqual("Secret Base", loc.LocationName);
            Assert.IsTrue(actual.Success);

        }

    }
}
