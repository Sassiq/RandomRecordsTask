namespace RandomRecordServices
{
    public interface IRandomRecordImportService
    {
        Task ImportRecords(IAsyncEnumerable<RandomRecord> records);
    }
}
