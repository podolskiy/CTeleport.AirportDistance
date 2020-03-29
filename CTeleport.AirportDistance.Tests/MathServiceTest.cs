using CTeleport.AirportDistance.Services.Contracts;
using CTeleport.AirportDistance.Services.Models;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace CTeleport.AirportDistance.Tests
{
    public class MathServiceTest
    {
        private readonly MathService _mathService;

        public MathServiceTest()
        {
            _mathService = new MathService();
        }
        [Theory]
        [InlineData(48.833333, 2.331938, 41.794594, 12.250346, 683.574916415028)]
        [InlineData(-18.333333, 35.933333, 48.833333, 2.331938, 5079.8750344285381)]
        [InlineData(45.665315, 9.698713, 42.787281, 141.681341, 5651.1188764711542)]
        [InlineData(14.744975, -17.490194, 37.615215, -122.389881, 6383.2153527734672)]
        public void CalculateDistanceTest(double srcLat, double srcLon, double dstLat, double dstLon, double expected)
        {
            var result = _mathService.CalculateDistance(new Location { Lat = srcLat, Lon = srcLon }, new Location { Lat = dstLat, Lon = dstLon },
                 DistanceMeasureType.miles);
            Assert.Equal(result, expected);
        }
    }
}