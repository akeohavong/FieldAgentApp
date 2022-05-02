using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FieldAgent.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionRepository _missionRepository;
        
        public MissionsController(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
        }

        [HttpGet, Authorize]
        [Route("/api/[controller]/{id}", Name="GetMission")]
        public IActionResult GetMission(int id)
        {
            var result = _missionRepository.Get(id);

            if (result.Success)
            {
                return Ok(result.Data);  
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("/api/[controller]/{agencyId}/agencymissions")]
        public IActionResult GetByAgency(int agencyId)
        {
            var result = _missionRepository.GetByAgency(agencyId);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet, Authorize]
        [Route("/api/[controller]/{agentId}/agentmissions")]
        public IActionResult GetByAgent(int agentId)
        {
            var result = _missionRepository.GetByAgent(agentId);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost, Authorize]
        public IActionResult AddMission(int agencyID,string codeName,string notes, DateTime start, DateTime projected )
        {
            Mission m = new Mission();
            m.AgencyID = agencyID;
            m.CodeName = codeName;
            m.Notes = notes;
            m.StartDate = start;
            m.ProjectedEndDate = projected;

            var result = _missionRepository.Insert(m);
            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetMission), new { id = m.MissionID }, m);
            }
            else
            {
                
                return BadRequest(result.Message);
            }
        }
        [HttpDelete("{id}"), Authorize]
        public IActionResult DeleteMission(int id)
        {
            if (!_missionRepository.Get(id).Success)
            {
                return NotFound($"Mission {id} not found");
            }
            var result = _missionRepository.Delete(id);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut, Authorize]
        public IActionResult EditMission(Mission mission)
        {
            if (!_missionRepository.Get(mission.MissionID).Success)
            {
                return NotFound($"Mission {mission.MissionID} not found");
            }
            var result = _missionRepository.Update(mission);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
