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
    public class AgentsController : ControllerBase
    {
        private readonly IAgentRepository _agentRepository;

        public AgentsController(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        [HttpGet, Authorize]
        [Route("/api/[controller]/{id}", Name = "GetAgent")]
        public IActionResult GetAgent(int id)
        {
            var result = _agentRepository.Get(id);
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
        [Route("/api/[controller]/{agentId}/missions")]
        public IActionResult GetMissions(int agentId)
        {
            var result = _agentRepository.GetMissions(agentId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost]
        public IActionResult AddAgent(ViewAgent viewAgent)
        {
            if (ModelState.IsValid)
            {
                Agent a = new Agent
                {
                    AgentID = viewAgent.agentId,
                    FirstName = viewAgent.firstName,
                    LastName = viewAgent.lastName,
                    DateOfBirth = viewAgent.dateOfBirth,
                    Height = viewAgent.height
                };

                var result = _agentRepository.Insert(a);
                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetAgent), new {id = result.Data.AgentID}, result.Data);
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

        [HttpPut, Authorize]
        public IActionResult UpdateAgent(ViewAgent viewAgent)
        {
            if(ModelState.IsValid && viewAgent.agentId > 0)
            {
                Agent a = new Agent
                {
                    AgentID = viewAgent.agentId,
                    FirstName = viewAgent.firstName,
                    LastName = viewAgent.lastName,
                    DateOfBirth = viewAgent.dateOfBirth,
                    Height = viewAgent.height
                };

                var findAgent = _agentRepository.Get(a.AgentID);
                if (!_agentRepository.Get(a.AgentID).Success)
                {
                    return NotFound($"Agent {a.AgentID} not found");
                }
                var result = _agentRepository.Update(a);
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
                if(viewAgent.agentId < 0)
                {
                    ModelState.AddModelError("agentId", "Invalid Agent ID");
                }
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult RemoveAgent(int id)
        {
            if (!_agentRepository.Get(id).Success)
            {
                return NotFound($"Agent {id} not found");
            }
            var result = _agentRepository.Delete(id);
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
