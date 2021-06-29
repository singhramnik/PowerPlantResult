using ConsoleClient.DataModel;
using ConsoleClient.DataModel.Input;

namespace ConsoleClient.Calculators
{
    public class OffShoreWind : PowerPlant
    {
        public override decimal GetDailyGenerationValue(Generator generator, Factor factor)
        {
            var total = 0.0m;
            generator.Generation.ForEach
            (
                g => total += g.Energy * g.Price * factor.ValueFactor.Low
            );

            return total;
        }
    }
}
