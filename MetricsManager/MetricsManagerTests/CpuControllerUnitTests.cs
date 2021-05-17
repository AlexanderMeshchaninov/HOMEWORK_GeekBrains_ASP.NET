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
    public class CpuControllerUnitTests
    {
        private readonly CpuMetricsController _controller;

        private readonly Mock<ILogger<CpuMetricsController>> _logger;

        private readonly Mock<ICpuMetricsAgentsRepository> _repository;

        private readonly IMapper _mapper;

        public CpuControllerUnitTests()
        {
            _logger = new Mock<ILogger<CpuMetricsController>>();

            _repository = new Mock<ICpuMetricsAgentsRepository>();

            _controller = new CpuMetricsController(_logger.Object, _repository.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgent()
        {
            //Arrange
            _repository.Setup(repository => repository
                .GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<CpuMetrics>()).Verifiable();

            //Act
            var result = _controller.GetMetricsFromAgent(new CpuMetricsApiRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            _repository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
