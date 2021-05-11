using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddControllerUnitTests
    {
        private HddMetricsController controller;

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

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
