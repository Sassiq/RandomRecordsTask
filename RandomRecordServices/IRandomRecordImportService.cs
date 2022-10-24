using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRecordServices
{
    public interface IRandomRecordImportService
    {
        Task ImportRecords(IAsyncEnumerable<RandomRecord> records);
    }
}
