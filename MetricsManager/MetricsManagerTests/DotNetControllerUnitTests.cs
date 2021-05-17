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
    public class DotNetControllerUnitTests
    {
        private readonly DotNetMetricsController _controller;

        private readonly Mock<ILogger<DotNetMetricsController>> _logger;

        private readonly Mock<IDotNetMetricsAgentsRepository> _repository;

        private readonly IMapper _mapper;

        public DotNetControllerUnitTests()
        {
            _logger = new Mock<ILogger<DotNetMetricsController>>();

            _repository = new Mock<IDotNetMetricsAgentsRepository>();

            _controller = new DotNetMetricsController(_logger.Object, _repository.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgent()
        {
            //Arrange
            _repository.Setup(repository => repository
                    .GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<DotNetMetrics>()).Verifiable();

            //Act
            var result = _controller.GetMetricsFromAgent(new DotNetMetricsApiRequest()
            {
                FromTime = DateTimeOffset.MinValue,
                ToTime = DateTimeOffset.UtcNow,
            });

            //Assert
            _repository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}