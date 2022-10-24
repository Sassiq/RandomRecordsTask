using System.Data;
using System.Data.SqlClient;

namespace RandomRecordServices.SqlServer
{
    public class RandomRecordImportService : IRandomRecordImportService
    {
        private readonly SqlConnection connection;
        public event EventHandler<ImportedRecordEventArgs>? RecordImported;

        public RandomRecordImportService(string connectionString)
        {
            this.connection = new SqlConnection(connectionString);
        }

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

                    OnRecordImported(new ImportedRecordEventArgs() { CurrentRecordNumber = count++, TotalRecordsNumber = 100_000 });
                }
            }
        }

        protected virtual void OnRecordImported(ImportedRecordEventArgs e)
        {
            RecordImported?.Invoke(this, e);
        }
    }
}