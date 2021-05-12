using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into NetworkMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(1, $"This log from GetMetricsFromAgent - agentId:{agentId}, fromTime:{fromTime}, toTime:{toTime}");

            return Ok();
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percintiles/{percentile}")]
        public IActionResult GetMetricsPercentilesFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(1, $"This log from GetMetricsPercentilesFromAgent - agentId:{agentId}, fromTime:{fromTime}, toTime:{toTime}");

            return Ok();
        }

        [HttpHead("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(1, $"This log from GetMetricsFromAllCluster - fromTime:{fromTime}, toTime:{toTime}");

            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation(1, $"This log from GetMetricsByPercentileFromAllCluster - fromTime:{fromTime}, toTime:{toTime}, percentile:{percentile}");

            return Ok();
        }
    }
}
