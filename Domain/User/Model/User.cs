using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.User.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }

        public string Username { get; set; }

        public string MacAdress { get; set; }

        public string Password { get; set; }

        public string State { get; set; }
    }
}
