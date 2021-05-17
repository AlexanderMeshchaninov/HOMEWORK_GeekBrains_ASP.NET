using MetricsManager.Controllers;
using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsManager.DAL;
using MetricsManager.Models;
using MetricsManager.Requests;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;


namespace MetricsManagerTests
{
    public class NetworkControllerUnitTests
    {
        private readonly NetworkMetricsController _controller;

        private readonly Mock<ILogger<NetworkMetricsController>> _logger;

        private readonly Mock<INetworkMetricsAgentsRepository> _repository;

        private readonly IMapper _mapper;

        public NetworkControllerUnitTests()
        {
            _logger = new Mock<ILogger<NetworkMetricsController>>();

            _repository = new Mock<INetworkMetricsAgentsRepository>();

            _controller = new NetworkMetricsController(_logger.Object, _repository.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgent()
        {
            //Arrange
            _repository.Setup(repository => repository
                    .GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<NetworkMetrics>()).Verifiable();

            //Act
            var result = _controller.GetMetricsFromAgent(new NetworkMetricsApiRequest()
            {
                FromTime = DateTimeOffset.MinValue,
                ToTime = DateTimeOffset.UtcNow,
            });

            //Assert
            _repository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}