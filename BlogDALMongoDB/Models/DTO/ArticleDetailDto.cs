using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Models.DTO
{
    public class ArticleDetailDTO
    {
        public string Name { get; set; }
        public string DisplayContent { get; set; }
        public int Category { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
