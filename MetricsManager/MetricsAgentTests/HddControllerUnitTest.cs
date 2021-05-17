using MetricsAgent.Controllers;
using System;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;

namespace MetricsAgentTests
{
    public class HddControllerUnitTests
    {
        private readonly HddMetricsController _controller;

        private readonly Mock<IHddMetricsRepository> _repository;

        private readonly Mock<ILogger<HddMetricsController>> _logger;

        private readonly IMapper _mapper;

        public HddControllerUnitTests()
        {
            _repository = new Mock<IHddMetricsRepository>();

            _logger = new Mock<ILogger<HddMetricsController>>();

            _controller = new HddMetricsController(_logger.Object, _repository.Object, _mapper);
        }

        [Fact]
        public void GetByTimePeriod_From_Controller()
        {
            //Arrange
            _repository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<HddMetrics>()).Verifiable();

            //Act
            var result = _controller.GetByTimePeriod(new HddMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _repository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
