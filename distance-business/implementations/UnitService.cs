using Distance.Business.Abstractions;
using Distance.Contracts;
using System.Globalization;

namespace Distance.Business.Implementations
{
    public class UnitService : IUnitService
    {
        public Units Get()
        {
            var name = CultureInfo.CurrentCulture.Name;

            switch (name)
            {
                case "en-US":
                    return Units.Miles;
                default:
                    return Units.Meters;
            }
        }
    }
}