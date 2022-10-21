using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LocalLangLibrary.Models
{
    public class BasisUserModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string DispalyName { get; set; }

        public BasisUserModel()
        {

        }

        public BasisUserModel(UserModel model)
        {
            Id = model.Id;
            DispalyName = model.DisplayName;
        }
    }
}
