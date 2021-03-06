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
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController controller;

<<<<<<< HEAD
        public DotNetControllerUnitTests()
        {
            controller = new DotNetMetricsController();
        }

        [Fact]
        public void GetMetricsErrorsCountFromAgent_ReturnOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsErrorsCountFromAgent(fromTime, toTime);
=======
        private Mock<IDotNetMetricsRepository> mock;

        private Mock<ILogger<DotNetMetricsController>> _logger;

        public DotNetControllerUnitTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();

            _logger = new Mock<ILogger<DotNetMetricsController>>();

            controller = new DotNetMetricsController(_logger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            //Устанавливаем параметры для заглушки
            //в заглушке прописываем, что в репозиторий прилетит CpuMetrics obj
            mock.Setup(repository => repository.Create(It.IsAny<DotNetMetrics>())).Verifiable();

            //Выполняем действие на контроллере
            var result = controller.Create(new DotNetMetricCreateRequest()
            {
                Time = DateTimeOffset.FromUnixTimeMilliseconds(1), 
                Value = 99,
            });

            mock.Verify(repository => repository.Create(It.IsAny<DotNetMetrics>()));
        }
        [Fact]
        public void Create_From_Controller_ReturnOk()
        {
            //Arrange
            var request = new DotNetMetricCreateRequest()
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
                .Returns(new List<DotNetMetrics>()).Verifiable();

            //Act
            var result = controller.GetByTimePeriod(new DotNetMetricGetByTimePeriodRequest()
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
