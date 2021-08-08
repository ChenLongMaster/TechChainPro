using System;

namespace BlogDAL.Models
{
    public class Article : ModelBase
    {
        public string Name { get; set; }
        public string Abstract { get; set; }
        public string DisplayContent { get; set; }
        public string RepresentImageUrl { get; set; }
        public int Rating { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid? CreatedBy { get; set; }
        public User CreatedByName { get; set; }
    }
}
