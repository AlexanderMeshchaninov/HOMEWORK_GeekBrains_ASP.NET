using MetricsManager.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsManager.Models;
using MetricsManager.Requests;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/manager/")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;

        private readonly IAgentsRepository _agentsRepository;

        public AgentsController(ILogger<AgentsController> logger, IAgentsRepository agentsRepository)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into AgentsController");

            _agentsRepository = agentsRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfoApiRequest request)
        {
            _logger.LogInformation(1, $"This log from RegisterAgent " +
                                      $"- agentId:{request.AgentId}, agentAddress:{request.AgentAddress}");

            _agentsRepository.Create(new AgentInfo
            {
                AgentId = request.AgentId,
                AgentAddress = request.AgentAddress
            });

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
