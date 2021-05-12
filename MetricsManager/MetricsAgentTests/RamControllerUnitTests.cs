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
        private RamMetricsController _controller;

        private Mock<IRamMetricsRepository> _mock;

        private Mock<ILogger<RamMetricsController>> _logger;

        private Mock<IMapper> _autoMapper;

        public RamControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();

            _logger = new Mock<ILogger<RamMetricsController>>();

            _autoMapper = new Mock<IMapper>();

            _controller = new RamMetricsController(_logger.Object, _mock.Object, _autoMapper.Object);
        }

        [Fact]
        public void GetByTimePeriod_From_Controller()
        {
            //Arrange
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<RamMetrics>()).Verifiable();

            //Act
            var result = _controller.GetByTimePeriod(new RamMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
