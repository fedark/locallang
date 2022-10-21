using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace LocalLangLibrary.Models
{
    public class ExpressionModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Expression { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public CategoryModel Category { get; set; }
        public BasisUserModel Author { get; set; }
        public HashSet<string> UserVotes { get; set; } = new();
        public StatusModel Status { get; set; }
        public string OwnerNotes { get; set; }
        public bool ApprovedForRelease { get; set; } = false;
        public bool Archived { get; set; } = false;
        public bool Rejected { get; set; } = false;
    }
}
