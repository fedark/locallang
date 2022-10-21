using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace LocalLangLibrary.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ObjectIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public List<BasicExpressionModel> AuthoredExpressions { get; set; } = new();
        public List<BasicExpressionModel> VotedOnExpressions { get; set; } = new();
    }
}
