namespace RandomRecordServices
{
    /// <summary>
    /// Importing service contract.
    /// </summary>
    public interface IRandomRecordImportService
    {
        Task ImportRecords(IAsyncEnumerable<RandomRecord> records);
    }
}
