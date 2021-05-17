using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.DAL;
using MetricsManager.Models;
using MetricsManager.Requests;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private readonly AgentsController controller;

        private readonly Mock<ILogger<AgentsController>> _logger;

        private readonly Mock<IAgentsRepository> _repository;

        public AgentsControllerUnitTests()
        {
            _logger = new Mock<ILogger<AgentsController>>();

            _repository = new Mock<IAgentsRepository>();

            controller = new AgentsController(_logger.Object, _repository.Object);
        }

        [Fact]
        public void RegisterAgent_ReturnOk()
        {
            //Arrange
            _repository.Setup(repository => repository.Create(It.IsAny<AgentInfo>())).Verifiable();

            //Act
            var result = controller.RegisterAgent(new AgentInfoApiRequest()
            {
                AgentId = 1,
                AgentAddress = "http://localhost:51684"
            });

            //Assert
            _repository.Verify(repository => repository.Create(It.IsAny<AgentInfo>()));
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
