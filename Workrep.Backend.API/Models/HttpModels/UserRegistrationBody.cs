using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workrep.Backend.API.Models.HttpModels
{
    public class UserRegistrationBody
    {
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
