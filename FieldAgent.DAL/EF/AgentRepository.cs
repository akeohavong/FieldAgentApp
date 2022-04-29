using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.EF
{
    public class AgentRepository : IAgentRepository
    {
        public DBFactory DbFac { get; set; }
        public AgentRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }
        public Response Delete(int agentId)
        {
            Response response = new Response();

            using (var db = DbFac.GetDbContext())
            {
                try
                {
                    Agent agent = db.Agent.Where(a => a.AgentID == agentId).Include(a => a.Aliases)
                        .Include(m => m.MissionAgent).Include(aa => aa.AgencyAgents).First();
                    db.Remove(agent);
                    db.SaveChanges();
                    response.Message = $"Agent {agentId} deleted.";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    response.Success = false;
                    response.Message = $"Error: {e.Message}";
                    return response;
                }

            }
            response.Success = true;
            return response;
        }

        public Response<Agent> Get(int agentId)
        {
            Response<Agent> response = new Response<Agent>();
            using (var db = DbFac.GetDbContext())
            {
                try
                {
                    response.Data = db.Agent.Find(agentId);
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

        public Response<List<Mission>> GetMissions(int agentId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();
            using (var db = DbFac.GetDbContext())
            {
                try
                {
                    response.Data = (from m in db.Mission
                                     join ma in db.MissionAgent on m.MissionID equals ma.MissionID
                                     where ma.AgentID == agentId
                                     select m).ToList();

                    if (response.Data == null)
                    {
                        response.Message = "No missions found";
                        response.Success = false;
                        return response;
                    }
                }
                catch(Exception e)
                {
                   
                    response.Success=false;
                    response.Message = e.Message;
                    return response;
                }
            }
            response.Success = true;
            return response;
        }

        public Response<Agent> Insert(Agent agent)
        {
            Response<Agent> response = new Response<Agent>();
            using (var db = DbFac.GetDbContext())
            {
                try
                {
                    response.Data = db.Agent.Add(agent).Entity;
                    db.SaveChanges();
                    response.Message = $"Agent {agent.AgentID} added";
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

        public Response Update(Agent agent)
        {
            Response<Agent> response = new Response<Agent>();
            using (var db = DbFac.GetDbContext())
            {
                try
                {
                    db.Agent.Update(agent);
                    db.SaveChanges();
                    response.Message = $"Agent {agent.AgentID} updated";
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
