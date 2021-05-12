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
        private HddMetricsController _controller;

        private Mock<IHddMetricsRepository> _mock;

        private Mock<ILogger<HddMetricsController>> _logger;

        private Mock<IMapper> _autoMapper;

        public HddControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();

            _logger = new Mock<ILogger<HddMetricsController>>();

            _autoMapper = new Mock<IMapper>();

            _controller = new HddMetricsController(_logger.Object, _mock.Object, _autoMapper.Object);
        }

        [Fact]
        public void GetByTimePeriod_From_Controller()
        {
            //Arrange
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<HddMetrics>()).Verifiable();

            //Act
            var result = _controller.GetByTimePeriod(new HddMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
