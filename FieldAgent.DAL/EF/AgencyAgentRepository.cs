using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces;

namespace FieldAgent.DAL.EF
{
    public class AgencyAgentRepository : IAgencyAgentRepository
    {
        public DBFactory dbf { get; set; }
        public AgencyAgentRepository(DBFactory dbfac)
        {
            dbf = dbfac;
        }
        public Response Delete(int agencyid, int agentid)
        {
            Response response = new();
            using(var db = dbf.GetDbContext())
            {
                try
                {
                    AgencyAgent aa = db.AgencyAgent.Where(a => a.AgencyID == agencyid && a.AgentID == agentid).First();
                    db.Remove(aa);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                    return response;
                }
            }
            response.Success = true;
            return response;
        }

        public Response<AgencyAgent> Get(int agencyid, int agentid)
        {
            Response<AgencyAgent> response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.AgencyAgent.Where(a => a.AgencyID == agencyid && a.AgentID == agentid).First();
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
                    return response;
                }
            }
            response.Success = true;
            return response;
        }

        public Response<List<AgencyAgent>> GetByAgency(int agencyId)
        {
            Response<List<AgencyAgent>> response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.AgencyAgent.Where(a => a.AgencyID == agencyId).ToList();
                    return response;
                }
                catch(Exception e)
                {
                    response.Message = e.Message;
                    response.Success = false;
                }
            }
            response.Success = true;
            return response;
        }

        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            Response<List<AgencyAgent>> response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.AgencyAgent.Where(a => a.AgentID == agentId).ToList();
                    return response;
                }
                catch (Exception e)
                {
                    response.Message = e.Message;
                    response.Success = false;
                }
            }
            response.Success = true;
            return response;
        }

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            Response<AgencyAgent> response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.AgencyAgent.Add(agencyAgent).Entity;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
                    return response;
                }
            }
            response.Success = true;
            return response;
        }

        public Response Update(AgencyAgent agencyAgent)
        {
            Response response = new Response();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    db.AgencyAgent.Update(agencyAgent);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
                    return response;
                }
            }
            response.Success = true;
            return response;
        }
    }
}
