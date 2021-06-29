using ConsoleClient.DataModel.Input;
using System;

namespace ConsoleClient.Calculators
{
    public static class CalculatorFactory
    {
        public static PowerPlant GetPlantCalculator(Generator generator)
        {
            if (generator is CoalGenerator) return new CoalPlant();

            if (generator is GasGenerator) return new GasPlant();

            if (!(generator is WindGenerator))
            {
                throw new SystemException("Unknown Power plant, cannot create the calculator");
            }

            if (((WindGenerator)generator).Location == "Offshore")
            {
                return new OffShoreWind();
            }
            else
            {
                return new OnShoreWind();
            }
        }
    }
}
