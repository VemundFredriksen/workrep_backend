using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workrep.Backend.API.Models
{
    public class UserLoginCredentials
    {

        public string Email { get; set; }
        public string Password { get; set; }

        //Might be interesting later
        public long PhoneNumber { get; set; }

    }
}
