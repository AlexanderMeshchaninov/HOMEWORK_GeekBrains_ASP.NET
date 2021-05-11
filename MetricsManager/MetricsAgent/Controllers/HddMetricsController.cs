<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

=======
﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.Extensions.Logging;
>>>>>>> Lesson-3_branch
namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
<<<<<<< HEAD
        [HttpGet("left/{freeMemoryHddSizeInMb}")]
        public IActionResult GetMetricsFromAgent([FromRoute] decimal freeMemoryHddSizeInMb)
        {
            return Ok();
        }
=======
        private readonly ILogger<HddMetricsController> _logger;

        private readonly IHddMetricsRepository _repository;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into HddMetricsController");

            _repository = repository;
        }

        //Все-таки решил добавить метод Create для mock тестов
        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _repository.Create(new HddMetrics()
            {
                Time = request.Time.ToUnixTimeMilliseconds(),
                Value = request.Value,
            });
            return Ok();
        }


        [HttpGet("getbytimeperiod")]
        public IActionResult GetByTimePeriod([FromBody] HddMetricGetByTimePeriodRequest request)
        {
            _logger.LogInformation(1, $"This log from GetByTimePeriod - fromTime:{request.FromTime}, toTime:{request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new HddMetricResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto()
                {
                    Time = DateTimeOffset.FromUnixTimeMilliseconds(metric.Time), 
                    Value = metric.Value, 
                    Id = metric.Id,
                });
            }

            return Ok(response);
        }
>>>>>>> Lesson-3_branch
    }
}
