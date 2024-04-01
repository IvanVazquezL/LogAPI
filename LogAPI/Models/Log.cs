using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LogAPI.Models
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        public string PageId { get; set; }

        public Log() { }
    }
}
