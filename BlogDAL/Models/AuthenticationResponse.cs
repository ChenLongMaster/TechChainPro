using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Models
{
    public class AuthenticationResponse : ModelBase
    {
        public AuthenticationResponse(User user,string token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
        }
        

        public string Username { get; set; }
        public string Token { get; set; }
        public List<Role> Roles { get; set; }

    }
}
