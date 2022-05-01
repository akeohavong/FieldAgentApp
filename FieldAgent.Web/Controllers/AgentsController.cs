﻿using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
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

        [HttpGet]
        [Route("/api/[controller]/{id}")]
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

        [HttpGet]
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
        public IActionResult AddAgent(string first, string last, DateTime dateOfBirth, decimal height)
        {
            Agent a = new Agent()
            {
                FirstName = first,
                LastName = last,
                DateOfBirth = dateOfBirth,
                Height = height
            };
            var result = _agentRepository.Insert(a);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateAgent(Agent agent)
        {
            if (!_agentRepository.Get(agent.AgentID).Success)
            {
                return NotFound($"Agent {agent.AgentID} not found");
            }
            var result = _agentRepository.Update(agent);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("{id}")]
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
