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
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController _controller;

        private Mock<IDotNetMetricsRepository> _mock;

        private Mock<ILogger<DotNetMetricsController>> _logger;

        private Mock<IMapper> _autoMapper;

        public DotNetControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();

            _logger = new Mock<ILogger<DotNetMetricsController>>();

            _autoMapper = new Mock<IMapper>();

            _controller = new DotNetMetricsController(_logger.Object, _mock.Object, _autoMapper.Object);
        }


        [Fact]
        public void GetByTimePeriod_From_Controller()
        {

            //Arrange
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<DotNetMetrics>()).Verifiable();

            //Act
            var result = _controller.GetByTimePeriod(new DotNetMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
