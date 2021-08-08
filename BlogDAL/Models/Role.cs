using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Models
{
    public class Role : ModelBase
    {
        public string Name { get; set; }
        public IEnumerable<User> User { get; set; }
    }
}
