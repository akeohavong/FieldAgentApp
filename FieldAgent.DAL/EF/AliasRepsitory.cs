
using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.EF
{
    public class AliasRepsitory : IAliasRepository
    {
        public DBFactory dbf { get; set; }
        public AliasRepsitory(DBFactory dbfac)
        {
            dbf = dbfac;
        }
        public Response Delete(int aliasId)
        {
            Response response = new();
            using(var db = dbf.GetDbContext())
            {
                try
                {
                    var alias = db.Alias.Find(aliasId);
                    db.Alias.Remove(alias);
                    db.SaveChanges();
                    response.Message = $"Alias {aliasId} deleted.";
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

        public Response<Alias> Get(int aliasId)
        {
            Response<Alias> response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.Alias.Find(aliasId);
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

        public Response<List<Alias>> GetByAgent(int agentId)
        {
            Response<List<Alias>> response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.Alias.Where(a => a.AgentID == agentId).ToList();
                    if (response.Data == null)
                    {
                        response.Message = "No aliases found.";
                        response.Success = false;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                    response.Success = false;
                }
            }
            response.Success = true;
            return response;
        }

        public Response<Alias> Insert(Alias alias)
        {
            Response<Alias> response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.Alias.Add(alias).Entity;
                    db.SaveChanges();
                    response.Message = $"Alias {alias.AliasID} added.";
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

        public Response Update(Alias alias)
        {
            Response response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    db.Alias.Update(alias);
                    db.SaveChanges();
                    response.Message = $"Alias {alias.AliasID} updated.";
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
    }
}
