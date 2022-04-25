using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.EF
{
    public class LocationRepository : ILocationRepository
    {
        public DBFactory DbFac { get; set; }
        public LocationRepository(DBFactory dbFac)
        {
            DbFac = dbFac;
        }
        public Response Delete(int locationId)
        {
            Response response = new();
            using(var db = DbFac.GetDbContext())
            {
                try
                {
                    var location = db.Location.Find(locationId);
                    db.Location.Remove(location);
                    db.SaveChanges();
                    response.Message = $"Location {locationId} deleted.";
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

        public Response<Location> Get(int locationId)
        {
            Response<Location> response = new();
            using(var db = DbFac.GetDbContext())
            {
                try
                {
                    response.Data = db.Location.Find(locationId);
                }
                catch(Exception ex)
                {
                    response.Success= false;
                    response.Message= ex.Message;
                    return response;
                }
            }
            response.Success = true;
            return response;
        }

        public Response<List<Location>> GetByAgency(int agencyId)
        {
            Response<List<Location>> response = new();
            using(var db = DbFac.GetDbContext())
            {
                try
                {
                    response.Data = db.Location.Where(l => l.AgencyID == agencyId).ToList();
                    if (response.Data == null)
                    {
                        response.Message = "No locations found";
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

        public Response<Location> Insert(Location location)
        {
            Response<Location> response = new();
            using(var db = DbFac.GetDbContext())
            {
                try
                {
                    response.Data = db.Location.Add(location).Entity;
                    db.SaveChanges();
                    response.Message = $"Location {location.LocationId} added.";
                }
                catch(Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                    return response;
                }
            }
            response.Success = true;
            return response;
        }

        public Response Update(Location location)
        {
            Response response = new();
            using(var db = DbFac.GetDbContext())
            {
                try
                {
                    db.Location.Update(location);
                    db.SaveChanges();
                    response.Message = $"Location {location.LocationId} updated.";
                }
                catch(Exception ex)
                {
                    response.Success=false;
                    response.Message = ex.Message;
                    return response;
                }
            }
            response.Success = true;
            return response;
        }
    }
}
