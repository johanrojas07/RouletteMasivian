namespace RouletteApi.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string RouletterCollectionName { get; set; }
        public string BetsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string RouletterCollectionName { get; set; }
        string BetsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
