using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
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

            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation(1, $"This log from EnableAgentById - agentId:{agentId}");

            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation(1, $"This log from DisableAgentById - agentId:{agentId}");

            return Ok();
        }
    }
}
