using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController controller;

        private Mock<IRamMetricsRepository> mock;

        private Mock<ILogger<RamMetricsController>> _logger;

        public RamControllerUnitTests()
        {
            mock = new Mock<IRamMetricsRepository>();

            _logger = new Mock<ILogger<RamMetricsController>>();

            controller = new RamMetricsController(_logger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            //Устанавливаем параметры для заглушки
            //в заглушке прописываем, что в репозиторий прилетит CpuMetrics obj
            mock.Setup(repository => repository.Create(It.IsAny<RamMetrics>())).Verifiable();

            //Выполняем действие на контроллере
            var result = controller.Create(new RamMetricCreateRequest()
            {
                Time = DateTimeOffset.FromUnixTimeMilliseconds(1), 
                Value = 99,
            });

            mock.Verify(repository => repository.Create(It.IsAny<RamMetrics>()));
        }
        [Fact]
        public void Create_From_Controller_ReturnOk()
        {
            //Arrange
            var request = new RamMetricCreateRequest()
            {
                Time = DateTimeOffset.FromUnixTimeMilliseconds(1),
                Value = 95,
            };

            //Act
            var result = controller.Create(request);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetByTimePeriod_From_Controller()
        {
            //Arrange
            mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<RamMetrics>()).Verifiable();

            //Act
            var result = controller.GetByTimePeriod(new RamMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
    }
}
