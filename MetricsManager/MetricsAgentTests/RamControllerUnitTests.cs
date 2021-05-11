using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController controller;

        public RamControllerUnitTests()
        {
            controller = new RamMetricsController();
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            //Arrange
            decimal freeMemoryRamSizeInMb = 140.0M;

            //Act
            var result = controller.GetMetricsFromAgent(freeMemoryRamSizeInMb);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
