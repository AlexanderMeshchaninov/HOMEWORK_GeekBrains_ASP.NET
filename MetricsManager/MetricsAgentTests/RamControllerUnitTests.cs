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
    public class RamControllerUnitTests
    {
        private readonly RamMetricsController _controller;

        private readonly Mock<IRamMetricsRepository> _repository;

        private readonly Mock<ILogger<RamMetricsController>> _logger;

        private readonly IMapper _mapper;

        public RamControllerUnitTests()
        {
            _repository = new Mock<IRamMetricsRepository>();

            _logger = new Mock<ILogger<RamMetricsController>>();

            _controller = new RamMetricsController(_logger.Object, _repository.Object, _mapper);
        }

        [Fact]
        public void GetByTimePeriod_From_Controller()
        {
            //Arrange
            _repository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<RamMetrics>()).Verifiable();

            //Act
            var result = _controller.GetByTimePeriod(new RamMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _repository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
