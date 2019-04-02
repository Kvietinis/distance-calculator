using System;
using Distance.Contracts;
using Distance.Business.Abstractions;

namespace Distance.Business.Implementations
{
    public class GreatCircleDistanceCalculator : IDistanceCalculator
    {
        public bool TryCalculate(Coordinate c1, Coordinate c2, Units unit, out double distance)
        {
            var longDistance = DiffLongitude(c1.Longitude, c2.Longitude);

            var arg1 = Math.Sin(c1.Latitude * Constants.DegreesToRadians) * Math.Sin(c2.Latitude * Constants.DegreesToRadians);
            var arg2 =
                Math.Cos(c1.Latitude * Constants.DegreesToRadians) *
                Math.Cos(c2.Latitude * Constants.DegreesToRadians) *
                Math.Cos(longDistance * Constants.DegreesToRadians);

            distance = Constants.MetersPerDegreeAtEquator * Math.Acos(arg1 + arg2) / Constants.DegreesToRadians;
            distance = unit == Units.Meters ? distance : Constants.MilesPerMeter * distance;

            return true;
        }

        private static double DiffLongitude(double l1, double l2)
        {
            double diff;

            if (l1 > 180.0)
            {
                l1 = 360.0 - l1;
            }

            if (l2 > 180.0)
            {
                l2 = 360.0 - l2;
            }
            
            if ((l1 >= 0.0) && (l2 >= 0.0))
            {
                diff = l2 - l1;
            }   
            else if ((l1 < 0.0) && (l2 < 0.0))
            {
                diff = l2 - l1;
            }
            else
            {
                if (l1 < 0)
                {
                    l1 = -1 * l1;
                }

                if (l2 < 0)
                {
                    l2 = -1 * l2;
                }

                diff = l1 + l2;

                if (diff > 180.0)
                {
                    diff = 360.0 - diff;
                }
            }

            return diff;
        }
    }
}