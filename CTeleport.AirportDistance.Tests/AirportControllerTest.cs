using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTeleport.AirportDistance.Api.Controllers;
using CTeleport.AirportDistance.Api.Models;
using CTeleport.AirportDistance.Services;
using CTeleport.AirportDistance.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Xunit;

namespace CTeleport.AirportDistance.Tests
{
    public class AirportControllerTest
    {

        [Fact]
        public async Task DistanceGetShouldHaveValidResult()
        {
            // Arrange
            var mockRepo = new Mock<IAirportService>();
            mockRepo
                .Setup(repo => repo.CalculateDistance(It.IsAny<DistanceModel>()))
                .ReturnsAsync(It.IsAny<double>);

            var controller = new AirportController(new Lazy<IAirportService>(mockRepo.Object));

            // Act
            var result = await controller.Distance(It.IsAny<DistanceRequest>());

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async Task Distance_ReturnsCompleteViewModel()
        {
            // Arrange
            var miles = (double) new Random().Next(0, Int32.MaxValue);
            var mockRepo = new Mock<IAirportService>();
            mockRepo
                .Setup(repo => repo.CalculateDistance(It.IsAny<DistanceModel>()))
                .ReturnsAsync(miles);
            var controller = new AirportController(new Lazy<IAirportService>(mockRepo.Object));

            // Act
            var actionResult = await controller.Distance(It.IsAny<DistanceRequest>());
            var model = (actionResult as OkObjectResult)?.Value as double?;

            // Assert
            Assert.Equal(model, miles);
        }

        [Fact]
        public async Task DistanceAction_RepositoryCalledOnce()
        {
            // Arrange
            var mockRepo = new Mock<IAirportService>();
            mockRepo
                .Setup(repo => repo.CalculateDistance(It.IsAny<DistanceModel>()))
                .ReturnsAsync(It.IsAny<double>);

            var controller = new AirportController(new Lazy<IAirportService>(mockRepo.Object));

            // Act
            var actionResult = await controller.Distance(It.IsAny<DistanceRequest>());

            // Assert
            mockRepo.Verify(_ => _.CalculateDistance(It.IsAny<DistanceModel>()), Times.Once);
        }
    }
}
