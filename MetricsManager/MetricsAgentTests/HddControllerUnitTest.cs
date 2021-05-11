using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
<<<<<<< HEAD
using Xunit;
=======
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
>>>>>>> Lesson-3_branch

namespace MetricsAgentTests
{
    public class HddControllerUnitTests
    {
        private HddMetricsController controller;

<<<<<<< HEAD
        public HddControllerUnitTests()
        {
            controller = new HddMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            //Arrange
            decimal freeMemoryHddSizeInMb = 134.05M;

            //Act
            var result = controller.GetMetricsFromAgent(freeMemoryHddSizeInMb);
=======
        private Mock<IHddMetricsRepository> mock;

        private Mock<ILogger<HddMetricsController>> _logger;

        public HddControllerUnitTests()
        {
            mock = new Mock<IHddMetricsRepository>();

            _logger = new Mock<ILogger<HddMetricsController>>();

            controller = new HddMetricsController(_logger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            //Устанавливаем параметры для заглушки
            //в заглушке прописываем, что в репозиторий прилетит CpuMetrics obj
            mock.Setup(repository => repository.Create(It.IsAny<HddMetrics>())).Verifiable();

            //Выполняем действие на контроллере
            var result = controller.Create(new HddMetricCreateRequest()
            {
                Time = DateTimeOffset.FromUnixTimeMilliseconds(1), 
                Value = 99,
            });

            mock.Verify(repository => repository.Create(It.IsAny<HddMetrics>()));
        }
        [Fact]
        public void Create_From_Controller_ReturnOk()
        {
            //Arrange
            var request = new HddMetricCreateRequest()
            {
                Time = DateTimeOffset.FromUnixTimeMilliseconds(1),
                Value = 95,
            };

            //Act
            var result = controller.Create(request);
>>>>>>> Lesson-3_branch

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
<<<<<<< HEAD
=======

        [Fact]
        public void GetByTimePeriod_From_Controller()
        {
            //Arrange
            mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(new List<HddMetrics>()).Verifiable();

            //Act
            var result = controller.GetByTimePeriod(new HddMetricGetByTimePeriodRequest()
            {
                FromTime = DateTimeOffset.FromUnixTimeMilliseconds(1),
                ToTime = DateTimeOffset.FromUnixTimeMilliseconds(100),
            });

            //Assert
            mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()));
        }
>>>>>>> Lesson-3_branch
    }
}
