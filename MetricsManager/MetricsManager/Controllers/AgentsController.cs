using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using Microsoft.Extensions.Logging;
>>>>>>> Lesson-3_branch

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
<<<<<<< HEAD
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
=======
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(ILogger<AgentsController> logger)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into AgentsController");
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation(1, $"This log from RegisterAgent - agentAddress:{agentInfo.AgentAddress}, agentId:{agentInfo.AgentId}");

>>>>>>> Lesson-3_branch
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
<<<<<<< HEAD
=======
            _logger.LogInformation(1, $"This log from EnableAgentById - agentId:{agentId}");

>>>>>>> Lesson-3_branch
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
<<<<<<< HEAD
=======
            _logger.LogInformation(1, $"This log from DisableAgentById - agentId:{agentId}");

>>>>>>> Lesson-3_branch
            return Ok();
        }
    }
}
