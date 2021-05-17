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
        private readonly CpuMetricsController _controller;

        private readonly Mock<ICpuMetricsRepository> _repository;

        private readonly Mock<ILogger<CpuMetricsController>> _logger;

        private readonly IMapper _mapper;

        public CpuControllerUnitTests()
        {
            _repository = new Mock<ICpuMetricsRepository>();

            _logger = new Mock<ILogger<CpuMetricsController>>();

            _controller = new CpuMetricsController(_logger.Object, _repository.Object, _mapper);
        }

        [Fact]
        public void GetByTimePeriod_From_Controller()
        {
            //Arrange
            _repository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(),It.IsAny<DateTimeOffset>()))
                .Returns(new List<CpuMetrics>()).Verifiable();

            //Act
            var result = _controller.GetByTimePeriod(new CpuMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _repository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
