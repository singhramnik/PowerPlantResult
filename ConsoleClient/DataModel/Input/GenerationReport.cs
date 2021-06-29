using System.Collections.Generic;
using System.Text;

namespace ConsoleClient.DataModel.Input
{
    /// <summary>
    /// Represents the xml Object of the input files
    /// </summary>
    public class GenerationReport
    {
        public List<WindGenerator> Wind { get; set; }
        public List<GasGenerator> Gas { get; set; }
        public List<CoalGenerator> Coal { get; set; }
    }
}
