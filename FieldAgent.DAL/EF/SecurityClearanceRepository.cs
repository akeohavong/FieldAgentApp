using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;


namespace FieldAgent.DAL.EF
{
    public class SecurityClearanceRepository : ISecurityClearanceRepository
    {
        public DBFactory dbf { get; set; }
        public SecurityClearanceRepository(DBFactory dbfac)
        {
            dbf = dbfac;
        }
        public Response<SecurityClearance> Get(int securityClearanceId)
        {
            Response<SecurityClearance> response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.SecurityClearance.Find(securityClearanceId);
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

        public Response<List<SecurityClearance>> GetAll()
        {
            Response<List<SecurityClearance>> response = new();
            using (var db = dbf.GetDbContext())
            {
                try
                {
                    response.Data = db.SecurityClearance.ToList();
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
