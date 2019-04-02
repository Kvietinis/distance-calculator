using System;
using EnsureThat;
using Distance.Contracts;
using Distance.Business.Abstractions;

namespace Distance.Business.Implementations
{
    public class DistanceService : IDistanceService
    {
        private readonly IDistanceCalculator[] _calculators;
        private readonly IUnitService _unitService;

        public DistanceService(IDistanceCalculator[] calculators, IUnitService unitService)
        {
            Ensure.That(calculators).IsNotNull();
            Ensure.That(unitService).IsNotNull();

            _calculators = calculators;
            _unitService = unitService;
        }

        public DistanceModel Calculate(Coordinate c1, Coordinate c2)
        {
            var units = _unitService.Get();

            foreach(var calculator in _calculators)
            {
                try
                {
                    if (calculator.TryCalculate(c1, c2, units, out double distance))
                    {
                        return new DistanceModel { Value = distance, Units = units };
                    }
                }
                catch (Exception e)
                {
                    var name = calculator.GetType().Name;
                    throw new CalculationException($"Fatal error occured with calculator '{name}'", e);
                }
            }

            throw new CalculationException("No valid calculator");
        }
    }
}