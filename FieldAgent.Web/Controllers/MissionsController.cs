using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.Web.Models;
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
        public IActionResult AddMission(ViewMission viewMission)
        {
            if (ModelState.IsValid)
            {
                Mission m = new Mission()
                {
                    AgencyID = viewMission.agencyId,
                    CodeName = viewMission.codename,
                    Notes = viewMission.notes,
                    StartDate = viewMission.startDate,
                    ProjectedEndDate = viewMission.projectedEndDate,
                    ActualEndDate = viewMission.actualEndDate,
                    OperationalCost = viewMission.operationalCost
                };

                var result = _missionRepository.Insert(m);
                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetMission), new { id = result.Data.MissionID }, result.Data);
                }
                else
                {

                    return BadRequest(result.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
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
        public IActionResult EditMission(ViewMission viewMission)
        {
            if(ModelState.IsValid && viewMission.missionId > 0)
            {
                Mission m = new Mission
                {
                    MissionID = viewMission.missionId,
                    AgencyID = viewMission.agencyId,
                    CodeName = viewMission.codename,
                    Notes = viewMission.notes,
                    StartDate = viewMission.startDate,
                    ProjectedEndDate = viewMission.projectedEndDate,
                    ActualEndDate = viewMission.actualEndDate,
                    OperationalCost = viewMission.operationalCost
                };

                var findMission = _missionRepository.Get(m.MissionID);
                if (!_missionRepository.Get(m.MissionID).Success)
                {
                    return NotFound($"Mission {m.MissionID} not found");
                }
                var result = _missionRepository.Update(m);
                if (result.Success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                if(viewMission.missionId < 1)
                {
                    ModelState.AddModelError("missionId", "Invalid Mission ID");
                }
                return BadRequest(ModelState);
            }
        }
            
            
    }
}
