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
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;

        private Mock<ICpuMetricsRepository> _mock;

        private Mock<ILogger<CpuMetricsController>> _logger;

        private Mock<IMapper> _autoMapper;

        public CpuControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();

            _logger = new Mock<ILogger<CpuMetricsController>>();

            _autoMapper = new Mock<IMapper>();

            _controller = new CpuMetricsController(_logger.Object, _mock.Object, _autoMapper.Object);
        }

        [Fact]
        public void GetByTimePeriod_From_Controller()
        {
            //Arrange
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(),It.IsAny<DateTimeOffset>()))
                .Returns(new List<CpuMetrics>()).Verifiable();

            //Act
            var result = _controller.GetByTimePeriod(new CpuMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
