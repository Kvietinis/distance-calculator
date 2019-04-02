using System;
using System.Linq;
using Distance.Business.Abstractions;
using Distance.Business.Implementations;
using SimpleInjector;

namespace Distance.Business.IoC
{
    public static class SimpleInjectorExtensions
    {
        public static void RegisterDistanceService(this Container container)
        {
            container.Collection.Register<IDistanceCalculator>(new [] { typeof(EuclidicDistanceCalculator), typeof(GreatCircleDistanceCalculator) });
            container.Register<IUnitService, UnitService>();
            container.Register<IDistanceService, DistanceService>();
        }
    }
}
