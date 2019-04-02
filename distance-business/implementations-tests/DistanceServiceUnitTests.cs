using System;
using Xunit;
using Distance.Business.Implementations;
using Moq;
using Distance.Business.Abstractions;
using Distance.Contracts;

namespace Distance.Business.Implementations.Tests
{
    public class DistanceServiceUnitTests
    {
        [Fact]
        public void ShouldThrowArgumentException_Constructor()
        {
            // arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new DistanceService(null, null));
            Assert.Throws<ArgumentNullException>(() => new DistanceService(It.IsAny<IDistanceCalculator[]>(), null));
        }

        [Fact]
        public void ShouldThrowCalculationException_OnCalculate_IfNoCalculator()
        {
            // arrange
            var unitService = new UnitServiceStub();
            var service = new DistanceService(new IDistanceCalculator[] {  }, unitService);
            var c1 = new Coordinate { Longitude = 0, Latitude = 0 };
            var c2 = new Coordinate { Longitude = 1, Latitude = 1 };

            // act and arrange
            Assert.Throws<CalculationException>(() => service.Calculate(c1, c2));
        }

        [Fact]
        public void ShouldThrowCalculationException_OnCalculate_IfCalculatorCannotCalculate()
        {
            // arrange
            var calculator = new DistanceFailureCalculatorStub();
            var unitService = new UnitServiceStub();
            var service = new DistanceService(new IDistanceCalculator[] { calculator }, unitService);
            var c1 = new Coordinate { Longitude = 0, Latitude = 0 };
            var c2 = new Coordinate { Longitude = 1, Latitude = 1 };

            // act and arrange
            Assert.Throws<CalculationException>(() => service.Calculate(c1, c2));
        }

        [Fact]
        public void ShouldNotFail_OnCalculate_IfValidCalculatorExists()
        {
            // arrange
            var calculator1 = new DistanceFailureCalculatorStub();
            var calculator2 = new DistanceSuccessCalculatorStub();
            var unitService = new UnitServiceStub();
            var service = new DistanceService(new IDistanceCalculator[] { calculator1, calculator2 }, unitService);
            var c1 = new Coordinate { Longitude = 0, Latitude = 0 };
            var c2 = new Coordinate { Longitude = 1, Latitude = 1 };

            // act
            var value = service.Calculate(c1, c2);

            // assert
            Assert.NotNull(value);
            Assert.Equal(Units.Meters, value.Units);
            Assert.Equal(10, value.Value);
        }
    }

    public class DistanceFailureCalculatorStub : IDistanceCalculator
    {
        public bool TryCalculate(Coordinate c1, Coordinate c2, Units unit, out double distance)
        {
            distance = 0;
            return false;
        }
    }

    public class DistanceSuccessCalculatorStub : IDistanceCalculator
    {
        public bool TryCalculate(Coordinate c1, Coordinate c2, Units unit, out double distance)
        {
            distance = 10;
            return true;
        }
    }

    public class UnitServiceStub : IUnitService
    {
        public Units Get()
        {
            return Units.Meters;
        }
    }
}
