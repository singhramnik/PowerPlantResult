using ConsoleClient.DataModel;
using ConsoleClient.DataModel.Input;
using System.Collections.Generic;

namespace ConsoleClient.Calculators
{
    public class CoalPlant : FossilFuelPlant<CoalGenerator>
    {
        public override List<DataModel.Output.Day> GetDailyEmissions(CoalGenerator generator, Factor factor)
        {
            var result = new List<DataModel.Output.Day>();

            generator.Generation.ForEach(g =>
            {
                result.Add(new DataModel.Output.Day
                {
                    Date = g.Date.ToString(),
                    Name = generator.Name,
                    Emission = g.Energy * generator.EmissionsRating * factor.EmissionsFactor.High
                });
            });

            return result;
        }

        public decimal ActualHeatRate(CoalGenerator generator)
        {
            return generator.TotalHeatInput / generator.ActualNetGeneration;
        }
    }
}
