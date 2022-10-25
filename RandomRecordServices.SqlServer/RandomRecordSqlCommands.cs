using System;
using System.Collections.Generic;
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
        public RandomRecordSqlCommands(SqlConnection connection)
        {
            this.connection = connection;
        }
    }
}
