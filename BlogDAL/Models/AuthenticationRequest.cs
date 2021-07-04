using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Models
{
    public class AuthenticationRequest : ModelBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
