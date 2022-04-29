using FieldAgent.Core;
using FieldAgent.Core.DTOs;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using FieldAgent.DAL.ADO;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests
{
    public class ReportsRepositoryTest
    {
        ReportsRepository db;
        DBFactory dbf;

        [SetUp]
        public void Setup()
        {
            dbf = new DBFactory();
            db = new ReportsRepository("Server=localhost;Database=FieldAgent;User Id=sa;Password=YOUR_strong_*pass4w0rd*;");
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGetTopAgents()
        {
            Response<List<TopAgentListItem>> response =  db.GetTopAgents();
            Assert.AreEqual(3, response.Data.Count);
        }

        [Test]
        public void TestAuditClearance()
        {
            Response<List<ClearanceAuditListItem>> response = db.AuditClearance(1, 3);
            Assert.AreEqual(1, response.Data.Count);
        }
        [Test]
        public void TestGetPensionList()
        {
            Response<List<PensionListItem>> response = db.GetPensionList(1);
            Assert.AreEqual(2, response.Data.Count);
        }
    }
}
