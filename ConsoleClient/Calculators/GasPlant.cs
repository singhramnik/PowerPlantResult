using ConsoleClient.DataModel;
using ConsoleClient.DataModel.Input;
using System.Collections.Generic;

namespace ConsoleClient.Calculators
{
    public class GasPlant : FossilFuelPlant<GasGenerator>
    {
        public override List<DataModel.Output.Day> GetDailyEmissions(GasGenerator generator, Factor factor)
        {
            var result = new List<DataModel.Output.Day>();

            generator.Generation.ForEach(g => 
            {
                result.Add(new DataModel.Output.Day
                {
                    Date = g.Date.ToString(),
                    Name = generator.Name,
                    Emission = g.Energy * generator.EmissionsRating * factor.EmissionsFactor.Medium
                });
            });

            return result;
        }
    }
}
