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
    public class RamControllerUnitTests
    {
        private readonly RamMetricsController _controller;

        private readonly Mock<ILogger<RamMetricsController>> _logger;

        private readonly Mock<IRamMetricsAgentsRepository> _repository;

        private readonly IMapper _mapper;

        public RamControllerUnitTests()
        {
            _logger = new Mock<ILogger<RamMetricsController>>();

            _repository = new Mock<IRamMetricsAgentsRepository>();

            _controller = new RamMetricsController(_logger.Object, _repository.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgent()
        {
            //Arrange
            _repository.Setup(repository => repository
                    .GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<RamMetrics>()).Verifiable();

            //Act
            var result = _controller.GetMetricsFromAgent(new RamMetricsApiRequest()
            {
                FromTime = DateTimeOffset.MinValue,
                ToTime = DateTimeOffset.UtcNow,
            });

            //Assert
            _repository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}