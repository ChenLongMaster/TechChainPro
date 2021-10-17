using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TechchainDAL.Models
{
    public class Article
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Abstract { get; set; }
        public string DisplayContent { get; set; }
        public string RepresentImageUrl { get; set; }
        public int Rating { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
