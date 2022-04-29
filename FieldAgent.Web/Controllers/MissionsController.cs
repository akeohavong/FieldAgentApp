using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.Web.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
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
    }
}
