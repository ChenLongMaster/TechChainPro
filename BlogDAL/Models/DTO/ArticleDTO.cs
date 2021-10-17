using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDALOld.Models.DTO
{
    [NotMapped]
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abstract { get; set; }
        public string DisplayContent { get; set; }
        public string RepresentImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int Rating { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
