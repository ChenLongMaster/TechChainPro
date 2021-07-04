using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogDAL.Models
{
    public class User : ModelBase
    {
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Salt { get; set; }
        public List<Role> Roles { get; set; }
    }
}
