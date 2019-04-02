using Xunit;
using Distance.Business.Implementations;
using Distance.Contracts;

namespace Distance.Business.Implementations.Tests
{
    public class EuclidicCalculatorUnitTests
    {
        [Fact]
        public void ShouldNot_Calculate_IfUtmZonesDifferTooMuch()
        {
            // arrange
            var distanceCalculator = new EuclidicDistanceCalculator();
            var lt = new Coordinate { Longitude = 23.8813, Latitude = 55.1694 };
            var ie = new Coordinate { Longitude = -8.2439, Latitude = 53.4129 };

            // act
            bool success = distanceCalculator.TryCalculate(lt, ie, Units.Meters, out double distance);

            // assert
            Assert.True(!success);
        }

        [Fact]
        public void Should_Calculate_InMeters_IfUtmZonesAreNearby()
        {
            // arrange
            var distanceCalculator = new EuclidicDistanceCalculator();
            var vil = new Coordinate { Longitude = 25.2797, Latitude = 54.6872 };
            var war = new Coordinate { Longitude = 21.2297, Latitude = 52.2297 };

            // act
            bool success = distanceCalculator.TryCalculate(vil, war, Units.Meters, out double distance);
            var intDistance = (int)distance;

            // assert
            Assert.True(success);
            Assert.Equal(383694, intDistance);
        }

        [Fact]
        public void Should_Calculate_InMiles_IfUtmZonesAreNearby()
        {
            // arrange
            var distanceCalculator = new EuclidicDistanceCalculator();
            var vil = new Coordinate { Longitude = 25.2797, Latitude = 54.6872 };
            var war = new Coordinate { Longitude = 21.2297, Latitude = 52.2297 };

            // act
            bool success = distanceCalculator.TryCalculate(vil, war, Units.Miles, out double distance);
            var intDistance = (int)distance;

            // assert
            Assert.True(success);
            Assert.Equal(238, intDistance);
        }
    }
}