using RandomRecordFileManagerTask;
using RandomRecordServices;
using RandomRecordServices.SqlServer;

namespace RandomRecordFileManagerTask
{
    internal class Program
    {
        const string path = @"G:\PROGRAMMING\Papka";
        const string connectionString = @"Server=localhost\SQLEXPRESS01;Database=RecordsDB;Trusted_Connection=True;";

        static async Task Main()
        {
            RandomRecordImportService service = new RandomRecordImportService(connectionString);
            service.RecordImported += OnRecordImported;

            RandomRecordFileManager manager = new RandomRecordFileManager(service, path);
            await manager.ImportFile("file#1.txt");
        }

        public static void OnRecordImported(object? o, ImportedRecordEventArgs e)
        {
            Console.WriteLine($"Imported {e.CurrentRecordNumber}, there are {e.TotalRecordsNumber - e.CurrentRecordNumber} records left.");
        }
    }
}