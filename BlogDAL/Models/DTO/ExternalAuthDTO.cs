using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Models.DTO
{
    public class ExternalAuthDTO
    {
        public string Provider { get; set; }
        public string Token { get; set; }
    }
}
