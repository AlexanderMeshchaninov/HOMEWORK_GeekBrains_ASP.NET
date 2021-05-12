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
    public class NetworkControllerUnitTests
    {
        private NetworkMetricsController _controller;

        private Mock<INetworkMetricsRepository> _mock;

        private Mock<ILogger<NetworkMetricsController>> _logger;

        private Mock<IMapper> _autoMapper;

        public NetworkControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();

            _logger = new Mock<ILogger<NetworkMetricsController>>();

            _autoMapper = new Mock<IMapper>();

            _controller = new NetworkMetricsController(_logger.Object, _mock.Object, _autoMapper.Object);
        }

        [Fact]
        public void GetByTimePeriod_From_Controller()
        {
            //Arrange
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<NetworkMetrics>()).Verifiable();

            //Act
            var result = _controller.GetByTimePeriod(new NetworkMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
