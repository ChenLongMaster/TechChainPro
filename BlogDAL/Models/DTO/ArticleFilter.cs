using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDALOld.Models.DTO
{
    public class ArticleFilter
    {
        public int CategoryId { get; set; }
        public int SortDateDirection { get; set; }
    }
}
