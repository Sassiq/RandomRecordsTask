using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRecordServices.SqlServer
{
    public class ImportedRecordEventArgs : EventArgs
    {
        public long TotalRecordsNumber { get; init; }
        public long CurrentRecordNumber { get; init; }
    }
}
