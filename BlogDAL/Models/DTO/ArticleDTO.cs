using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Models.DTO
{
    public class ArticleDTO : ModelBase
    {
        public string Name { get; set; }
        public string Abstract { get; set; }
        public string DisplayContent { get; set; }
        public string RepresentImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int Rating { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }

    }
}
