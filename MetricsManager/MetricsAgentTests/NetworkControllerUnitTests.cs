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
        private readonly NetworkMetricsController _controller;

        private readonly Mock<INetworkMetricsRepository> _repository;

        private readonly Mock<ILogger<NetworkMetricsController>> _logger;

        private readonly IMapper _mapper;

        public NetworkControllerUnitTests()
        {
            _repository = new Mock<INetworkMetricsRepository>();

            _logger = new Mock<ILogger<NetworkMetricsController>>();

            _controller = new NetworkMetricsController(_logger.Object, _repository.Object, _mapper);
        }

        [Fact]
        public void GetByTimePeriod_From_Controller()
        {
            //Arrange
            _repository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<NetworkMetrics>()).Verifiable();

            //Act
            var result = _controller.GetByTimePeriod(new NetworkMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _repository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
