using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Models.DTO
{
    public class ArticleDTO
    {
        public string Name { get; set; }
        public string Abstract { get; set; }
        public string DisplayContent { get; set; }
        public IFormFile representImage { get; set; }
        public int Category { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
