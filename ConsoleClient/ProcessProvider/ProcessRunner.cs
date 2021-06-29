using System;
using System.IO;
using ConsoleClient.DataModel;
using ConsoleClient.DataModel.Input;
using ConsoleClient.DataModel.Output;
using ConsoleClient.Calculators;
using System.Collections.Generic;

namespace ConsoleClient.ProcessProvider
{
    public class ProcessRunner
    {
        public void RunTask(string refDataFile, string inputPath, string outputPath)
        {
            var serialzer = new Serialzer();
            var xmlRefData = File.ReadAllText(refDataFile);
            var refData = serialzer.Deserializer<ReferenceData>(xmlRefData);

            //Get list of Files
            var files = Directory.GetFiles(inputPath);
            foreach (var file in files)
            {
                var outputFileName = @$"{outputPath}\{Path.GetFileNameWithoutExtension(file)}-Result.xml";

                ProcessSingleFile(refData, file, outputFileName);

                //TODO: Once it has been Processed move it to Processed or Rejected folder
            }
        }

        private void ProcessSingleFile(ReferenceData refData, string inputFile, string outputFileName)
        {
            var serialzer = new Serialzer();
               
            var xmlInputData = File.ReadAllText(inputFile);
            var inputData = serialzer.Deserializer<GenerationReport>(xmlInputData);


            var output = ProcessData(refData, inputData);

            var outputStr = serialzer.Serialize(output);
#if DEBUG
            Console.WriteLine(outputStr);
#endif

            //At the moment it will just overwrite any existing file,
            //good idea to suggest renaming old file if same file already exists
            File.WriteAllText(outputFileName, outputStr);
        }

        /// <summary>
        /// This is the main processor responsible for calling the write class for calculations. 
        /// </summary>
        private static GenerationOutput ProcessData(ReferenceData refData, GenerationReport inputData)
        {
            //Create output object for collection all the information. 
            var output = new GenerationOutput
            {
                Totals = new List<DataModel.Output.Generator>()
            };

            ProcessWindData(refData, inputData, output);

            output.MaxEmissionGenerators = new List<DataModel.Output.Day>();
            ProcessGasData(refData, inputData, output);

            output.ActualHeatRates = new List<ActualHeatRate>();
            ProcessCoalData(refData, inputData, output);

            return output;
        }

        private static void ProcessCoalData(ReferenceData refData, GenerationReport inputData, GenerationOutput output)
        {
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
        }

        private static void ProcessGasData(ReferenceData refData, GenerationReport inputData, GenerationOutput output)
        {
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
        }

        private static void ProcessWindData(ReferenceData refData, GenerationReport inputData, GenerationOutput output)
        {
            inputData.Wind.ForEach(w =>
            {
                var calculator = CalculatorFactory.GetPlantCalculator(w);

                output.Totals.Add(new DataModel.Output.Generator
                {
                    Name = w.Name,
                    Total = calculator.GetDailyGenerationValue(w, refData.Factors)
                });
            });
        }
    }
}
