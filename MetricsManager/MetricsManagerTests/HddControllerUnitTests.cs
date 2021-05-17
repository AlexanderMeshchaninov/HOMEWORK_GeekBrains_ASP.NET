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
    public class HddControllerUnitTests
    {
        private readonly HddMetricsController _controller;

        private readonly Mock<ILogger<HddMetricsController>> _logger;

        private readonly Mock<IHddMetricsAgentsRepository> _repository;

        private readonly IMapper _mapper;

        public HddControllerUnitTests()
        {
            _logger = new Mock<ILogger<HddMetricsController>>();

            _repository = new Mock<IHddMetricsAgentsRepository>();

            _controller = new HddMetricsController(_logger.Object, _repository.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgent()
        {
            //Arrange
            _repository.Setup(repository => repository
                    .GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<HddMetrics>()).Verifiable();

            //Act
            var result = _controller.GetMetricsFromAgent(new HddMetricsApiRequest()
            {
                FromTime = DateTimeOffset.MinValue,
                ToTime = DateTimeOffset.UtcNow,
            });

            //Assert
            _repository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}