using System;
using Distance.Contracts;
using Distance.Business.Abstractions;
using NetTopologySuite;
using NetTopologySuite.Operation.Distance;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;
using GeoAPI.Geometries;
using GeoAPI.CoordinateSystems.Transformations;
using GeoCoordinate = GeoAPI.Geometries.Coordinate;
using Coordinate = Distance.Contracts.Coordinate;

namespace Distance.Business.Implementations
{
    public class EuclidicDistanceCalculator : IDistanceCalculator
    {
        private IGeometryFactory _factory;

        public EuclidicDistanceCalculator()
        {
            _factory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        }

        public bool TryCalculate(Coordinate c1, Coordinate c2, Units unit, out double distance)
        {
            distance = 0;

            if (TryGetTransformation(c1, c2, out ICoordinateTransformation transformation))
            {
                var geoCoord1 = Transform(c1, transformation);
                var geoCoord2 = Transform(c2, transformation);

                var point1 = _factory.CreatePoint(geoCoord1);
                var point2 = _factory.CreatePoint(geoCoord2);

                distance = DistanceOp.Distance(point1, point2);
                distance = unit == Units.Meters ? distance : Constants.MilesPerMeter * distance;

                return true;
            }

            return false;
        }

        private static bool TryGetTransformation(Coordinate c1, Coordinate c2, out ICoordinateTransformation transformation)
        {
            var result = false;
            transformation = null;

            int utmZone1 = GetUtmZone(c1.Longitude);
            int utmZone2 = GetUtmZone(c2.Longitude);

            bool isSouth1 = c1.Latitude < 0;
            bool isSouth2 = c2.Latitude < 0;

            result = (Math.Abs((utmZone1 - utmZone2))) < 2 && (isSouth1 == isSouth2);

            if (!result)
            {
                return false;
            }

            var wgs = GeographicCoordinateSystem.WGS84;
            var utm = ProjectedCoordinateSystem.WGS84_UTM(utmZone1, !isSouth1);

            transformation = new CoordinateTransformationFactory().CreateFromCoordinateSystems(wgs, utm);

            return result;
        }

        private static GeoCoordinate Transform(Coordinate c, ICoordinateTransformation transformation)
        {
            return transformation.MathTransform.Transform(ToGeoCoordinate(c));
        }

        private static GeoCoordinate ToGeoCoordinate(Coordinate c)
        {
            return new GeoCoordinate(c.Longitude, c.Latitude);
        }

        private static int GetUtmZone(double longitude)
        {
            return (int)(((longitude + 180) / 6) % 60) + 1;
        }
    }
}