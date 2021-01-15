using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ReadingIsGood.Domain.Documents
{
    // Document Base Entity
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
    }
}
