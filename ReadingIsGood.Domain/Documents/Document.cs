using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ReadingIsGood.Domain.Documents
{
    // Document Base Entity
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
