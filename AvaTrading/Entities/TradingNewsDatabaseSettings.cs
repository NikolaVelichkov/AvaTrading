namespace AvaTrading.Entities
{
    public class TradingNewsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TradingNewsCollectionName { get; set; } = null!;
    }
}
