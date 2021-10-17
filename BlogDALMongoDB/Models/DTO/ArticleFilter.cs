using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechchainDAL.Models.DTO
{
    public class ArticleFilter
    {
        public int CategoryId { get; set; }
        public int SortDateDirection { get; set; }
    }
}
