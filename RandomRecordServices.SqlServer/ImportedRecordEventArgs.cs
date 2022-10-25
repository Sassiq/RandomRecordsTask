namespace RandomRecordServices.SqlServer
{
    public class ImportedRecordEventArgs : EventArgs
    {
        public long TotalRecordsNumber { get; init; }
        public long CurrentRecordNumber { get; init; }
    }
}
