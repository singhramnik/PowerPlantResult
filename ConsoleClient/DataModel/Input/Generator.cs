using System.Collections.Generic;

namespace ConsoleClient.DataModel.Input
{
    public class Generator
    {
        public string Name { get; set; }

        public List<Day> Generation { get; set; }
    }

    public class WindGenerator : Generator
    {
        public string Location { get; set; }
    }

    public class GasGenerator : Generator
    {
        public decimal EmissionsRating { get; set; }
    }

    public class CoalGenerator : GasGenerator
    {
        public decimal TotalHeatInput { get; set; }
        public decimal ActualNetGeneration { get; set; }
    }
}
