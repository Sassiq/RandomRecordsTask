using Microsoft.Extensions.Configuration;
using RandomRecordServices.SqlServer;

namespace RandomRecordsTask.ConsoleApp
{
    internal class Program
    {
        static async Task Main()
        {
            // Configuration. Get connection string to the database. Get working directory path.
            SettingsConfig config = new SettingsConfig();
            string connectionString = config.ConfigurationRoot.GetConnectionString("RecordsDB");
            string path = config.ConfigurationRoot["workingDirectory"];

            // Create new RandomRecords importing service.
            RandomRecordImportService service = new RandomRecordImportService(connectionString);

            // Add importing progress through console ouput.
            service.RecordImported += OnRecordImported;

            // Create new RandomRecords file manager.
            RandomRecordFileManager manager = new RandomRecordFileManager(service, path);
            
            // This manager can generate files with RandomRecords in string format.
            int numberOfFiles = 100, numberOfRecords = 100_000;
            await manager.GenerateFiles(numberOfFiles, numberOfRecords);
            Console.WriteLine($"Generated {numberOfFiles} files with {numberOfRecords} lines.");

            // Manager can combine these files into one file.
            string combinedFile = "combined.txt";
            await manager.CombineFiles(numberOfFiles, combinedFile);
            Console.WriteLine($"Successfully combined {numberOfFiles} files into {combinedFile}.");

            //Manager can combine these files into one and delete strings with the given substring.
            string deletableSubstring = "abc", cuttedCombinedFile = "cuttedCombined.txt";
            int deletedCount = await manager.CombineFiles(numberOfFiles, cuttedCombinedFile, deletableSubstring);
            Console.WriteLine($"Successfully combined {numberOfFiles} files into {cuttedCombinedFile}. {deletedCount} strings were deleted.");
            
            //Manager can import file. We have SQL importing service, so it imports into database.
            await manager.ImportFile("RandomRecords#1.txt");
        }

        private static void OnRecordImported(object? o, ImportedRecordEventArgs e)
        {
            // Total records number could be counted synchronously with IEnumerable.Count() instead of IAsyncEnumerable usage
            // in RandomRecordImportService.ImportRecords, but it will harm perfomance.
            // Console output of the progress have already harmed performance.
            int totalRecordsNumber = 100_000;
            Console.WriteLine($"Imported {e.CurrentRecordNumber}, there are {totalRecordsNumber - e.CurrentRecordNumber} records left.");
        }
    }
}