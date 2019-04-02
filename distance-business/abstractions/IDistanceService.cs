using Distance.Contracts;

namespace Distance.Business.Abstractions
{
    public interface IDistanceService
    {
        DistanceModel Calculate(Coordinate c1, Coordinate c2);
    }
}