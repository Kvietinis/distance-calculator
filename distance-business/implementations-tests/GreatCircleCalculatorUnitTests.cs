using Distance.Contracts;
using Xunit;

namespace Distance.Business.Implementations.Tests
{
    public class GreatCircleCalculatorUnitTests
    {
        [Fact]
        public void Should_Calculate_InMeters_IfUtmZonesAreNearby()
        {
            // arrange
            var distanceCalculator = new GreatCircleDistanceCalculator();
            var lt = new Coordinate { Longitude = 23.8813, Latitude = 55.1694 };
            var ie = new Coordinate { Longitude = -8.2439, Latitude = 53.4129 };

            // act
            bool success = distanceCalculator.TryCalculate(lt, ie, Units.Meters, out double distance);
            var intDistance = (int)distance;

            // assert
            Assert.True(success);
            Assert.Equal(2077820, intDistance);
        }

        [Fact]
        public void Should_Calculate_InMiles_IfUtmZonesAreNearby()
        {
            // arrange
            var distanceCalculator = new GreatCircleDistanceCalculator();
            var lt = new Coordinate { Longitude = 23.8813, Latitude = 55.1694 };
            var ie = new Coordinate { Longitude = -8.2439, Latitude = 53.4129 };

            // act
            bool success = distanceCalculator.TryCalculate(lt, ie, Units.Miles, out double distance);
            var intDistance = (int)distance;

            // assert
            Assert.True(success);
            Assert.Equal(1291, intDistance);
        }
    }
}