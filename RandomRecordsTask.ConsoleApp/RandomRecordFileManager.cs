using RandomRecordServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRecordsTask.ConsoleApp
{
    /// <summary>
    /// Class which operates with file system and RandomRecords.
    /// </summary>
    public class RandomRecordFileManager
    {
        private readonly IRandomRecordImportService importService;
        private string workingDirectory;

        /// <summary>
        /// Initializes a new instance of <see cref="RandomRecordFileManager"/> class.
        /// </summary>
        /// <param name="service">Importing service.</param>
        /// <param name="workingDirectory">Directory file manager will be operating.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RandomRecordFileManager(IRandomRecordImportService service, string workingDirectory)
        {
            this.importService = service ?? throw new ArgumentNullException(nameof(service));
            this.WorkingDirectory = workingDirectory;
        }

        /// <summary>
        /// Gets or sets working directory.
        /// </summary>
        public string WorkingDirectory
        {
            get => this.workingDirectory;
            set
            {
                this.workingDirectory = value ?? throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Combines RandomRecords files in "combinedFile.txt".
        /// </summary>
        /// <param name="numberOfFiles">Number of RandomRecords files.</param>
        /// <param name="combinedFileName">Name of the output combined file.</param>
        /// <param name="deletableString">Ignorable strings.</param>
        /// <returns>Number of deleted lines.</returns>
        public async Task<int> CombineFiles(int numberOfFiles, string combinedFileName, string deletableString = "")
        {
            int count = 0;
            using (StreamWriter sw = new StreamWriter(Path.Combine(WorkingDirectory, combinedFileName), new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.ReadWrite }))
            {
                for (int i = 0; i < numberOfFiles; i++)
                {
                    string currentFile = Path.Combine(WorkingDirectory, $"RandomRecords#{i + 1}.txt");
                    if (!File.Exists(currentFile))
                    {
                        throw new ArgumentException($"File {currentFile} is not exist.");
                    }

                    using (StreamReader sr = new StreamReader(currentFile, new FileStreamOptions() { Mode = FileMode.Open, Access = FileAccess.Read }))
                    {
                        string? currentString;
                        while ((currentString = await sr.ReadLineAsync()) != null)
                        {
                            if (!string.IsNullOrEmpty(deletableString) && currentString.Contains(deletableString))
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

        /// <summary>
        /// Generates "RandomRecords#[numberOfFile].txt" files.
        /// </summary>
        /// <param name="numberOfFiles">Number of files being generated.</param>
        /// <param name="numberOfRecords">Number of records in file being generated.</param>
        /// <returns></returns>
        public async Task GenerateFiles(int numberOfFiles, int numberOfRecords)
        {
            for (int i = 0; i < numberOfFiles; i++)
            {
                string currentFile = Path.Combine(WorkingDirectory, $"RandomRecords#{i + 1}.txt");
                await GenerateFile(currentFile, numberOfRecords);
            }
        }

        /// <summary>
        /// Imports file.
        /// </summary>
        /// <param name="fileName">Name of the file being imported.</param>
        /// <returns></returns>
        public async Task ImportFile(string file)
        {
            await this.importService.ImportRecords(GetRecords(Path.Combine(WorkingDirectory, file)));
        }

        private async IAsyncEnumerable<RandomRecord> GetRecords(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath, new FileStreamOptions() { Mode = FileMode.Open, Access = FileAccess.Read }))
            {
                string? currentString;
                while ((currentString = await sr.ReadLineAsync()) != null)
                {
                    yield return ParseRecord(currentString);
                }
            }
        }

        private static async Task GenerateFile(string filePath, int numberOfRecords)
        {
            using (StreamWriter sw = new StreamWriter(filePath, new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.ReadWrite }))
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
