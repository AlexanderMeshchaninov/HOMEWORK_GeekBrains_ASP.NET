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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
<<<<<<< HEAD
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
=======
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into HddMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(1, $"This log from GetMetricsFromAgent - agentId:{agentId}, fromTime:{fromTime}, toTime:{toTime}");

>>>>>>> Lesson-3_branch
            return Ok();
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percintiles/{percentile}")]
        public IActionResult GetMetricsPercentilesFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
<<<<<<< HEAD
=======
            _logger.LogInformation(1, $"This log from GetMetricsPercentilesFromAgent - agentId:{agentId}, fromTime:{fromTime}, toTime:{toTime}");

>>>>>>> Lesson-3_branch
            return Ok();
        }

        [HttpHead("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
<<<<<<< HEAD
=======
            _logger.LogInformation(1, $"This log from GetMetricsFromAllCluster - fromTime:{fromTime}, toTime:{toTime}");

>>>>>>> Lesson-3_branch
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] Percentile percentile)
        {
<<<<<<< HEAD
=======
            _logger.LogInformation(1, $"This log from GetMetricsByPercentileFromAllCluster - fromTime:{fromTime}, toTime:{toTime}, percentile:{percentile}");

>>>>>>> Lesson-3_branch
            return Ok();
        }
    }
}
