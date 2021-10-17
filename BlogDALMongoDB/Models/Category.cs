using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BlogDAL.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
