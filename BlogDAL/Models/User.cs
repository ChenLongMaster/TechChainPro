using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlogDALOld.Models
{
    public class User : ModelBase
    {
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Provider { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
