using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.EF
{
    public class MissionRepository : IMissionRepository
    {
        public DBFactory dbf { get; set; }

        public MissionRepository(DBFactory dbfac)
        {
            dbf = dbfac;
        }
        public Response Delete(int missionId)
        {
            Response response = new();

            using(var db = dbf.GetDbContext())
            {
                try
                {
                    Mission mission = db.Mission.Where(m => m.MissionID == missionId)
                        .Include(ma => ma.MissionAgent).First();

                    db.Remove(mission);
                    db.SaveChanges();
                    response.Message = $"Mission {missionId} deleted.";
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

        public Response<Mission> Get(int missionId)
        {
            Response<Mission> response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.Mission.Find(missionId);
                    if(response.Data != null)
                    {
                        response.Success = true;
                    }
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
                }
            }
            return response;
        }

        public Response<List<Mission>> GetByAgency(int agencyId)
        {
            Response<List<Mission>> response = new();
            using(var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.Mission.Where(m => m.AgencyID == agencyId).ToList();
                    if(response.Data == null)
                    {
                        response.Message = "No missions found";
                        response.Success = false;
                    }
                    else
                    {
                        response.Success =true;
                    }
                }
                catch(Exception e)
                {
                    response.Message = e.Message;
                    response.Success=false; 
                }
            }

            response.Success = true;           
            return response;
        }

        public Response<List<Mission>> GetByAgent(int agentId)
        {
            Response<List<Mission>> response = new();
            using (var db = dbf.GetDbContext())
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
                    }
                    else
                    {
                        response.Success = true;
                    }
                }
                catch (Exception e)
                {
                    response.Message = e.Message;
                    response.Success = false;
                }
            }
            return response;
        }

        public Response<Mission> Insert(Mission mission)
        {
            Response<Mission> response = new Response<Mission>();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.Mission.Add(mission).Entity;
                    db.SaveChanges();
                    response.Success = true;
                    response.Message = $"Mission {mission.MissionID} added.";
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
                  
                }
            }
            
            return response;
        }


        public Response Update(Mission mission)
        {
            Response response = new Response();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    db.Mission.Update(mission);
                    db.SaveChanges();
                    response.Success=true;
                    response.Message = $"Mission {mission.MissionID} updated.";
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
              
                }
            }
        
            return response;
        }
    }
}
