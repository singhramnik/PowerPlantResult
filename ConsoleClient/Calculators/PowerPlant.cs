using ConsoleClient.DataModel;
using ConsoleClient.DataModel.Input;

namespace ConsoleClient.Calculators
{
    /// <summary>
    /// An abstract class used by Wind(Onshore & offShore, Gas & Coal calculators)
    /// </summary>
    public abstract class PowerPlant
    {
        /// <summary>
        /// Abstract function overridden by inherited classes to Calculate Total Generation Value
        /// </summary>
        public abstract decimal GetDailyGenerationValue(Generator generator, Factor factor);
    }
}
