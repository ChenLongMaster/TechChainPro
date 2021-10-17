using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace BlogDAL.Models
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
