using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LocalLangLibrary.Models
{
    public class BasicExpressionModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }

        public BasicExpressionModel()
        {

        }

        public BasicExpressionModel(ExpressionModel model)
        {
            Id = model.Id;
            Title = model.Expression;
        }
    }
}
