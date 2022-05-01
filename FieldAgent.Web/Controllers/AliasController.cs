using FieldAgent.Core.Interfaces.DAL;
using Microsoft.AspNetCore.Mvc;
using FieldAgent.Core.Entities;
using System;

namespace FieldAgent.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliasController : ControllerBase
    {
        
        private readonly IAliasRepository _aliasRepository;
        private readonly IAgentRepository _agentRepository;

        public AliasController(IAliasRepository aliasRepository, IAgentRepository agentRepository)
        {
            _aliasRepository = aliasRepository;
            _agentRepository = agentRepository;
        }

        [HttpGet]
        [Route("/api/[controller]/{id}")]
        public IActionResult GetAlias(int id)
        {
            var result = _aliasRepository.Get(id);
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
        [Route("/api/[controller]/{agentId}/aliases")]
        public IActionResult GetByAgent(int agentId)
        {
            var result = _aliasRepository.GetByAgent(agentId);
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
        public IActionResult AddAlias(int agentId, string aliasName, string persona)
        {
            Alias a = new Alias()
            {
                AgentID = agentId,
                AliasName = aliasName,
                Persona = persona,
                InterpolID = Guid.NewGuid()
            };

            var result = _aliasRepository.Insert(a);

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
        public IActionResult UpdateAlias(Alias alias)
        {
            if(!_aliasRepository.Get(alias.AliasID).Success)
            {
                return NotFound($"Alias {alias.AliasID} not found");
            }
            var result = _aliasRepository.Update(alias);
            if(result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAlias(int id)
        {
            if (!_aliasRepository.Get(id).Success)
            {
                return NotFound($"Alias {id} not found");
            }
            var result = _aliasRepository.Delete(id);
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
