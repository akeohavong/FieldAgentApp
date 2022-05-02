using FieldAgent.Core.Interfaces.DAL;
using Microsoft.AspNetCore.Mvc;
using FieldAgent.Core.Entities;
using System;
using Microsoft.AspNetCore.Authorization;
using FieldAgent.Web.Models;

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

        [HttpGet, Authorize]
        [Route("/api/[controller]/{id}", Name = "GetAlias")]
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

        [HttpGet, Authorize]
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

        [HttpPost, Authorize]
        public IActionResult AddAlias(ViewAlias viewAlias)
        {
            if (ModelState.IsValid)
            {
                Alias a = new Alias()
                {
                    AgentID = viewAlias.agentId,
                    AliasName = viewAlias.aliasName,
                    Persona = viewAlias.persona,
                    InterpolID = Guid.NewGuid()
                };

                var result = _aliasRepository.Insert(a);

                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetAlias), new {id = result.Data.AliasID}, result.Data);
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
        public IActionResult UpdateAlias(ViewAlias viewAlias)
        {
            if(ModelState.IsValid && viewAlias.aliasId > 0)
            {
                Alias a = new Alias()
                {
                    AliasID = viewAlias.aliasId,
                    AgentID = viewAlias.agentId,
                    AliasName = viewAlias.aliasName,
                    Persona = viewAlias.persona,
                    InterpolID = Guid.NewGuid()
                };

                var findAlias = _aliasRepository.Get(a.AliasID);
                if (!_aliasRepository.Get(a.AliasID).Success)
                {
                    return NotFound($"Alias {a.AliasID} not found");
                }
                var result = _aliasRepository.Update(a);
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
                if(viewAlias.aliasId < 1)
                {
                    ModelState.AddModelError("aliasId", "Invalid Alias ID");
                }
            }
            return BadRequest(ModelState);  
        }

        [HttpDelete("{id}"), Authorize]
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
