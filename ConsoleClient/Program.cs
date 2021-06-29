using System;
using System.Configuration;
using System.IO;
using ConsoleClient.DataModel;
using ConsoleClient.DataModel.Input;
using ConsoleClient.DataModel.Output;
using ConsoleClient.Calculators;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            var serialzer = new Serialzer();

            var refDataPath = ConfigurationManager.AppSettings["ReferenceDataFile"];
            var xmlRefData = File.ReadAllText(refDataPath);
            var refData = serialzer.Deserializer<ReferenceData>(xmlRefData);

            var inputPath = ConfigurationManager.AppSettings["DataInputFolder"] + @"\01-Basic.xml";

            var xmlInputData = File.ReadAllText(inputPath);
            var inputData = serialzer.Deserializer<GenerationReport>(xmlInputData);

            var outputStr = ProcessData(refData, inputData);
            
            Console.WriteLine(outputStr);

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

        private static string ProcessData(ReferenceData refData, GenerationReport inputData)
        {
            var outputPath = ConfigurationManager.AppSettings["DataOutputFolder"] + @"\01-Basic-Result.xml";

            var serialzer = new Serialzer();

            var output = new GenerationOutput
            {
                Totals = new System.Collections.Generic.List<DataModel.Output.Generator>()
            };

            inputData.Wind.ForEach(w => 
            {
                var calculator = CalculatorFactory.GetPlantCalculator(w);

                output.Totals.Add(new DataModel.Output.Generator
                {
                    Name = w.Name,
                    Total = calculator.GetDailyGenerationValue(w, refData.Factors)
                });
            });

            output.MaxEmissionGenerators = new System.Collections.Generic.List<DataModel.Output.Day>();

            inputData.Gas.ForEach(g =>
            {
                GasPlant calculator = (GasPlant)CalculatorFactory.GetPlantCalculator(g);
                output.Totals.Add(new DataModel.Output.Generator 
                {
                    Name = g.Name,
                    Total = calculator.GetDailyGenerationValue(g, refData.Factors)
                });

                output.MaxEmissionGenerators.AddRange(calculator.GetDailyEmissions(g, refData.Factors));
            });

            output.ActualHeatRates = new System.Collections.Generic.List<ActualHeatRate>();

            inputData.Coal.ForEach(c =>
            {
                var calculator = (CoalPlant)CalculatorFactory.GetPlantCalculator(c);
                output.Totals.Add(new DataModel.Output.Generator
                {
                    Name = c.Name,
                    Total = calculator.GetDailyGenerationValue(c, refData.Factors)
                });

                output.MaxEmissionGenerators.AddRange(calculator.GetDailyEmissions(c, refData.Factors));

                output.ActualHeatRates.Add(new ActualHeatRate
                {
                    Name = c.Name,
                    HeatRate = calculator.ActualHeatRate(c)
                });
            });

            var outputStr = serialzer.Serialize(output);

            File.WriteAllText(outputPath, outputStr);

            return outputStr;
        }
    }
}
