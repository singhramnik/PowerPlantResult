using ConsoleClient.DataModel;
using ConsoleClient.DataModel.Input;
using System.Collections.Generic;

namespace ConsoleClient.Calculators
{
    /// <summary>
    /// Abstract class so that Gas & Coal can independently calculate DailyEmissions
    /// </summary>
    public abstract class FossilFuelPlant<T>: PowerPlant
    {
        public override decimal GetDailyGenerationValue(Generator generator, Factor factor)
        {
            var total = 0.0m;
            generator.Generation.ForEach
            (
                g => total += g.Energy * g.Price * factor.ValueFactor.Medium
            );

            return total;
        }

        public abstract List<DataModel.Output.Day> GetDailyEmissions(T generator, Factor factor);
    }
}
