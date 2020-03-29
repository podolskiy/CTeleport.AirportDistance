using System;
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
        private readonly AirportController _controller;
        public AirportControllerTest()
        {
            var mockRepo = new Mock<IAirportService>();
            mockRepo.Setup(repo => repo.CalculateDistance(It.IsAny<DistanceModel>())).ReturnsAsync(It.IsAny<double>);

            _controller = new AirportController(mockRepo.Object);
        }

        [Fact]
        public async Task DistanceGetShouldHaveValidResult()
        {
            var result = await _controller.Distance(It.IsAny<DistanceRequest>());

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }
    }
}
