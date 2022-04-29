using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.EF
{
    public class AgencyRepository : IAgencyRepository
    {
        public DBFactory DbFac { get; set; }

        public AgencyRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }
        public Response Delete(int agencyId)
        {
            Response response = new Response();

            using (var db = DbFac.GetDbContext())
            {
                try
                {
                    Agency agency = db.Agency.Find(agencyId);
                    db.Agency.Remove(agency);
                    db.SaveChanges();
                    response.Message = $"Agency {agencyId} deleted.";
                }
                catch(Exception e)
                {
                    response.Success = false;
                    response.Message = $"Error: {e.Message}";
                    return response;
                }

            }
            response.Success = true;
            return response;
        }



        public Response<Agency> Get(int agencyId)
        {
            Response<Agency> response = new Response<Agency>();
            using (var db = DbFac.GetDbContext())
            {
                try
                {
                    response.Data = db.Agency.Find(agencyId);
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
        public Response<List<Agency>> GetAll()
        {
            Response<List<Agency>> response = new Response<List<Agency>>();
            using (var db = DbFac.GetDbContext())
            {
                try
                {
                    response.Data = db.Agency.ToList();
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

        public Response<Agency> Insert(Agency agency)
        {
            Response<Agency> response = new Response<Agency>();
            using (var db = DbFac.GetDbContext())
            {
                try
                {
                    response.Data = db.Agency.Add(agency).Entity;
                    db.SaveChanges();
                    response.Message = $"Agency {agency.AgencyID} added.";
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

        public Response Update(Agency agency)
        {
            Response response = new Response();
            using(var db = DbFac.GetDbContext())
            {
                try
                {
                    db.Agency.Update(agency);
                    db.SaveChanges();
                    response.Message = $"Agency {agency.AgencyID} updated.";
                }
                catch(Exception e)
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
