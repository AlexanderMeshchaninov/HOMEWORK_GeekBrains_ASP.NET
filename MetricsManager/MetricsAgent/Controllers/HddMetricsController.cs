using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Models;
using MetricsAgent.DAL;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;

        private readonly IHddMetricsRepository _repository;

        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into HddMetricsController");

            _repository = repository;

            _mapper = mapper;
        }

        //Все-таки решил добавить метод Create для mock тестов
        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _repository.Create(new HddMetrics()
            {
                Time = request.Time,
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
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
