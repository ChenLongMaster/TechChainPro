﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDALOld.Models
{
    public class AuthenticationResponse 
    {
        public AuthenticationResponse(string token)
        {
           
            Token = token;
        }

        public string Token { get; set; }

    }
}
