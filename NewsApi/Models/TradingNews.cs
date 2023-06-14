using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NewsApi.Models
{
    [BsonIgnoreExtraElements]
    public class TradingNews
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("id")]
        public string id { get; set; }
        public Publisher publisher { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string published_utc { get; set; }
        public string article_url { get; set; }
        public List<string> tickers { get; set; }
        public string amp_url { get; set; }
        public string image_url { get; set; }
        public string description { get; set; }
        public List<string> keywords { get; set; }
    }
}
