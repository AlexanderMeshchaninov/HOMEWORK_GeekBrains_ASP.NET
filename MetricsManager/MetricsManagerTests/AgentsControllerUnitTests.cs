using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
<<<<<<< HEAD
=======
using Moq;
using Microsoft.Extensions.Logging;
>>>>>>> Lesson-3_branch

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;

<<<<<<< HEAD
        public AgentsControllerUnitTests()
        {
            controller = new AgentsController();
=======
        private Mock<ILogger<AgentsController>> _logger;

        public AgentsControllerUnitTests()
        {
            _logger = new Mock<ILogger<AgentsController>>();

            controller = new AgentsController(_logger.Object);
>>>>>>> Lesson-3_branch
        }

        [Fact]
        public void RegisterAgent_ReturnOk()
        {
            //Arrange
            var agentInfo = new MetricsManager.AgentInfo();

            //Act
            var result = controller.RegisterAgent(agentInfo);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnOk()
        {
            //Arrange
            var agentId = 5;

            //Act
            var result = controller.EnableAgentById(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnOk()
        {
            //Arrange
            var agentId = 5;

            //Act
            var result = controller.DisableAgentById(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
