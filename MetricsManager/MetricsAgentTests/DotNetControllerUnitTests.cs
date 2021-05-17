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
        private readonly DotNetMetricsController _controller;

        private readonly Mock<IDotNetMetricsRepository> _repository;

        private readonly Mock<ILogger<DotNetMetricsController>> _logger;

        private readonly IMapper _mapper;

        public DotNetControllerUnitTests()
        {
            _repository = new Mock<IDotNetMetricsRepository>();

            _logger = new Mock<ILogger<DotNetMetricsController>>();

            _controller = new DotNetMetricsController(_logger.Object, _repository.Object, _mapper);
        }


        [Fact]
        public void GetByTimePeriod_From_Controller()
        {

            //Arrange
            _repository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<DotNetMetrics>()).Verifiable();

            //Act
            var result = _controller.GetByTimePeriod(new DotNetMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _repository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
