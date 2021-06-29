using ConsoleClient.DataModel;
using ConsoleClient.DataModel.Input;

namespace ConsoleClient.Calculators
{
    public class OnShoreWind : PowerPlant
    {
        public override decimal GetDailyGenerationValue(Generator generator, Factor factor)
        {
            var total = 0.0m;
            generator.Generation.ForEach
            (
                g => total += g.Energy * g.Price * factor.ValueFactor.High
            );

            return total;
        }
    }
}
