using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbAccess;
using MongoDbAccess.Models;
using System.Diagnostics.CodeAnalysis;

namespace LocalLangLibrary.Models
{
    [CachedCollection(1440)]
    public class Category : IModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [NotNull]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

		public Category(string name)
		{
			Name = name;
		}

        public Category(string name, string description)
        {
            Name = name;
			Description = description;
		}
    }
}
