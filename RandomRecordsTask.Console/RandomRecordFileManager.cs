using RandomRecordServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRecordFileManagerTask
{
    public class RandomRecordFileManager
    {
        private readonly IRandomRecordImportService importService;
        private readonly string path;

        public RandomRecordFileManager(IRandomRecordImportService service, string workingDirectory)
        {
            this.importService = service;
            this.path = workingDirectory;
        }

        public async Task<int> CombineFiles(string deletableString)
        {
            int count = 0;
            using (StreamWriter sw = new StreamWriter(Path.Combine(path, "combinedFile.txt"), new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.ReadWrite }))
            {
                for (int i = 0; i < 100; i++)
                {
                    string currentFile = Path.Combine(path, $"file#{i + 1}.txt");
                    using (StreamReader sr = new StreamReader(currentFile, new FileStreamOptions() { Mode = FileMode.Open, Access = FileAccess.Read }))
                    {
                        string? currentString;
                        while ((currentString = await sr.ReadLineAsync()) != null)
                        {
                            if (currentString.Contains(deletableString))
                            {
                                count++;
                            }
                            else
                            {
                                await sw.WriteLineAsync(currentString);
                            }
                        }
                    }
                }
            }

            return count;
        }

        public async Task GenerateFiles(int number, string fileName)
        {
            for (int i = 0; i < number; i++)
            {
                string currentFile = Path.Combine(path, $"{fileName}#{i + 1}.txt");
                await GenerateFile(currentFile);
            }
        }

        public async Task ImportFile(string fileName)
        {
            await this.importService.ImportRecords(GetRecords(fileName));
        }

        private async IAsyncEnumerable<RandomRecord> GetRecords(string fileName)
        {
            using (StreamReader sr = new StreamReader(Path.Combine(path, fileName), new FileStreamOptions() { Mode = FileMode.Open, Access = FileAccess.Read }))
            {
                string? currentString;
                while ((currentString = await sr.ReadLineAsync()) != null)
                {
                    yield return ParseRecord(currentString);
                }
            }
        }

        private static async Task GenerateFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path, new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.ReadWrite }))
            {
                for (int j = 1; j < 100_000; j++)
                {
                    await sw.WriteLineAsync(new RandomRecord().ToString());
                }

                await sw.WriteAsync(new RandomRecord().ToString());
            }
        }

        private static RandomRecord ParseRecord(string str)
        {
            string[] components = str.Split("||", StringSplitOptions.RemoveEmptyEntries);
            return new RandomRecord(
                Convert.ToDateTime(components[0]),
                components[1],
                components[2],
                Convert.ToInt32(components[3]),
                Convert.ToDouble(components[4]));
        }
    }
}
