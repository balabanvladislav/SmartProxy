namespace UTM.Service.Settigs
{
    public class SyncServiceOptions
    {
        public string Host { get; set; } = string.Empty;
        public string UpsertHttpMethod { get; set; } = string.Empty;
        public string DeleteHttpMethod { get; set; } = string.Empty;
    }
}
