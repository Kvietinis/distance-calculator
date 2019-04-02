using Distance.Contracts;

namespace Distance.Business.Abstractions
{
    public interface IDistanceCalculator
    {
        bool TryCalculate(Coordinate c1, Coordinate c2, Units unit, out double distance);
    }
}