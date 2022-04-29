using FieldAgent.Core;
using FieldAgent.Core.DTOs;
using FieldAgent.Core.Interfaces.DAL;
using System.Data;
using System.Data.SqlClient;

namespace FieldAgent.DAL.ADO
{
    public class ReportsRepository : IReportsRepository
    {
        public string ConnectionString { get; set; }
        public ReportsRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            Response<List<ClearanceAuditListItem>> response = new();
            List<ClearanceAuditListItem> clearanceAudits = new List<ClearanceAuditListItem>();
            using(var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = "ClearanceAudit",
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                    };
                    SqlParameter param1 = new SqlParameter()
                    {
                        ParameterName = "@securityClearanceId",
                        SqlDbType = SqlDbType.Int,
                        Value = securityClearanceId,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter param2 = new SqlParameter()
                    {
                        ParameterName = "@agencyId",
                        SqlDbType = SqlDbType.Int,
                        Value = agencyId,
                        Direction = ParameterDirection.Input
                    };

                    cmd.Parameters.Add(param1);
                    cmd.Parameters.Add(param2);
                   

                    connection.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ClearanceAuditListItem row = new ClearanceAuditListItem();
                            row.BadgeId = (Guid)dr["BadgeId"];
                            row.NameLastFirst = $"{dr["LastName"]}, {dr["FirstName"]}";
                            row.DateOfBirth = (DateTime)dr["DateOfBirth"];
                            row.ActivationDate = (DateTime)dr["ActivationDate"];
                            row.DeactivationDate = (DateTime)dr["DeactivationDate"];

                            clearanceAudits.Add(row);
                        }
                    }
                    if(clearanceAudits.Count == 0)
                    {
                        Console.WriteLine("No records found");
                    }
                    else
                    {
                        foreach(var row in clearanceAudits)
                        {
                            Console.WriteLine($"{row.BadgeId} | {row.NameLastFirst} | {row.DateOfBirth} | {row.ActivationDate} | {row.DeactivationDate}");
                        }
                    }
                }
                
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }

            }
            response.Data = clearanceAudits;
            response.Success = true;
            
            return response;
        }

        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            Response<List<PensionListItem>> response = new();
            List<PensionListItem> PensionList = new();
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = "GetPensionList",
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                    };
                    SqlParameter param1 = new SqlParameter()
                    {
                        ParameterName = "@agencyId",
                        SqlDbType = SqlDbType.Int,
                        Value = agencyId,
                        Direction = ParameterDirection.Input
                    };

                    cmd.Parameters.Add(param1);
                    connection.Open();
                    using(var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PensionListItem row = new PensionListItem();
                            row.AgencyName = $"{dr["shortname"]}";
                            row.BadgeId = (Guid)dr["BadgeId"];
                            row.NameLastFirst = $"{dr["LastName"]}, {dr["FirstName"]}";
                            row.DateOfBirth = (DateTime)dr["DateOfBirth"];
                            row.DeactivationDate = (DateTime)dr["DeactivationDate"];

                            PensionList.Add(row);
                        }
                    }
                    foreach(var row in PensionList)
                    {
                        Console.WriteLine($"{row.AgencyName} | {row.BadgeId} | {row.NameLastFirst} | {row.DateOfBirth} | {row.DeactivationDate}");
                    }

                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }
            response.Data = PensionList;
            response.Success = true;
            return response; 
        }

        public Response<List<TopAgentListItem>> GetTopAgents()
        {
            Response<List<TopAgentListItem>> response = new Response<List<TopAgentListItem>>();
             List<TopAgentListItem> topAgents = new();
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    var command = new SqlCommand("GetTopAgents", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using(var dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                           
                            var row = new TopAgentListItem();

                            row.NameLastFirst = $"{dr["LastName"]}, {dr["FirstName"]}";
                            row.DateOfBirth = (DateTime)dr["DateOfBirth"];
                            row.CompletedMissionCount = (int)dr["MissionsCompleted"];
                            
                            topAgents.Add(row);
                        }
                    }
                    foreach(var row in topAgents)
                    {
                        Console.WriteLine($"{row.NameLastFirst} {row.DateOfBirth} {row.CompletedMissionCount}");
                    }
                    response.Data = topAgents;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }
            response.Success = true;
            return response;
        }
    }
}
