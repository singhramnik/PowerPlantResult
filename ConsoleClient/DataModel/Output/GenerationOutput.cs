using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClient.DataModel.Output
{
    public class GenerationOutput
    {
        public List<Generator> Totals { get; set; }
        public List<Day> MaxEmissionGenerators { get; set; }

        public List<ActualHeatRate> ActualHeatRates { get; set; }
    }
}
