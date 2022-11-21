using MongoDB.Bson.Serialization.Attributes;

namespace UTM.Repository.MongoDB.Models
{
    public abstract class MongoDocument
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime LastChangedAt { get; set; }
    }
}
