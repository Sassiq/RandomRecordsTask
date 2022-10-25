using System.Data;
using System.Data.SqlClient;

namespace RandomRecordServices.SqlServer
{
    /// <summary>
    /// Importing service for <see cref="RandomRecord"/>.
    /// </summary>
    public class RandomRecordImportService : IRandomRecordImportService
    {
        private readonly SqlConnection connection;
        public EventHandler<ImportedRecordEventArgs>? RecordImported;

        /// <summary>
        /// Initializes a new instance of <see cref="RandomRecordImportService"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string to database.</param>
        public RandomRecordImportService(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            this.connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Imports collection of records into database.
        /// </summary>
        /// <param name="records">Collection of <see cref="RandomRecord"/></param>
        /// <returns></returns>
        public async Task ImportRecords(IAsyncEnumerable<RandomRecord> records)
        {
            using (var sqlCommand = new SqlCommand("PostRecords", this.connection) { CommandType = CommandType.StoredProcedure })
            {
                sqlCommand.Parameters.Add("@Date", SqlDbType.Date);
                sqlCommand.Parameters.Add("@EnglishString", SqlDbType.NVarChar);
                sqlCommand.Parameters.Add("@RussianString", SqlDbType.NVarChar);
                sqlCommand.Parameters.Add("@IntegerNumber", SqlDbType.Int);
                sqlCommand.Parameters.Add("@FloatingNumber", SqlDbType.Float);

                await this.connection.OpenAsync();

                int count = 1;
                await foreach (RandomRecord record in records)
                {
                    sqlCommand.Parameters["@Date"].Value = record.Date.Date;
                    sqlCommand.Parameters["@EnglishString"].Value = record.EnglishString;
                    sqlCommand.Parameters["@RussianString"].Value = record.RussianString;
                    sqlCommand.Parameters["@IntegerNumber"].Value = record.IntegerNumber;
                    sqlCommand.Parameters["@FloatingNumber"].Value = record.FloatingNumber;
                    await sqlCommand.ExecuteNonQueryAsync();

                    OnRecordImported(new ImportedRecordEventArgs() { CurrentRecordNumber = count++ });
                }

                await this.connection.CloseAsync();
            }
        }

        protected virtual void OnRecordImported(ImportedRecordEventArgs e)
        {
            RecordImported?.Invoke(this, e);
        }
    }
}