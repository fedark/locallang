using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace LocalLangLibrary.Models
{
    public class Category
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
