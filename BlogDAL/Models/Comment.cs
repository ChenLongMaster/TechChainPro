using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDALOld.Models
{
    public class Comment : ModelBase
    {
        public Guid ParentId { get; set; }
        public Guid ArticleId { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
    }
}
