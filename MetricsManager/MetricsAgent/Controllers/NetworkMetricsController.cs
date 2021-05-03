using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        private readonly INetworkMetricsRepository _repository;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into NetworkMetricsController");

            _repository = repository;
        }

        //Все-таки решил добавить метод Create для mock тестов
        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _repository.Create(new NetworkMetrics()
            {
                Time = request.Time.ToUnixTimeMilliseconds(),
                Value = request.Value,
            });
            return Ok();
        }

        [HttpGet("getbytimeperiod")]
        public IActionResult GetByTimePeriod([FromBody] NetworkMetricGetByTimePeriodRequest request)
        {
            _logger.LogInformation(1, $"This log from GetByTimePeriod - fromTime:{request.FromTime}, toTime:{request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new NetworkMetricResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto()
                {
                    Time = DateTimeOffset.FromUnixTimeMilliseconds(metric.Time), 
                    Value = metric.Value, 
                    Id = metric.Id,
                });
            }

            return Ok(response);
        }
    }
}
