using System;
using System.Configuration;
using ConsoleClient.ProcessProvider;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var refDataFile = ConfigurationManager.AppSettings["ReferenceDataFile"];
            var inputPath = ConfigurationManager.AppSettings["DataInputFolder"];
            var outputPath = ConfigurationManager.AppSettings["DataOutputFolder"];

            if (args.Length > 0 && args[0].ToLower() == "-service")
            {
                //Call (new ProcessRunner()).RunTask() in an infinite Loop
            }
            else
            {
                //this can be run singly or as a scheduled task using window's scheduler
                (new ProcessRunner()).RunTask(refDataFile, inputPath, outputPath);
            }

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
