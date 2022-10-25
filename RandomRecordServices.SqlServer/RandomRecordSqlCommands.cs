using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRecordServices.SqlServer
{
    /// <summary>
    /// Class which use database stored procedures in RecordsDb.
    /// </summary>
    public class RandomRecordSqlCommands
    {
        private readonly SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of <see cref="RandomRecordSqlCommands"/> class.
        /// </summary>
        /// <param name="connection"></param>
        public RandomRecordSqlCommands(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new ArgumentNullException(nameof(connection));
            }

            this.connection = new SqlConnection(connection);
        }

        /// <summary>
        /// Counts sum of all random records integer numbers in database.
        /// </summary>
        /// <returns>Sum of numbers.</returns>
        public async Task<long> GetIntegerSum()
        {
            long result = 0;

            using (var sqlCommand = new SqlCommand("CountSum", this.connection) { CommandType = CommandType.StoredProcedure })
            {
                await this.connection.OpenAsync();
                var reader = await sqlCommand.ExecuteReaderAsync();
                
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    result = (long)reader["sum"];
                }

                await this.connection.CloseAsync();
            }

            return result;
        }

        /// <summary>
        /// Gets median of all random records floating numbers.
        /// </summary>
        /// <returns>Median of floating numbers.</returns>
        public async Task<double> GetMedian()
        {
            double result = 0.0;

            using (var sqlCommand = new SqlCommand("CountMedian", this.connection) { CommandType = CommandType.StoredProcedure })
            {
                await this.connection.OpenAsync();
                var reader = await sqlCommand.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    result = (double)reader["median"];
                }

                await this.connection.CloseAsync();
            }

            return result;
        }
    }
}
