using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workrep.Backend.DatabaseIntegration.Models;

namespace Workrep.Backend.API.Models
{
    public class ClientUser
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public int UserId { get; set; }
        public bool Confirmed { get; set; }
        public DateTime RegisterDate { get; set; }

        public ClientUser(User user)
        {
            Email = user.Email;
            Name = user.Name;
            Gender = user.Gender;
            Birthdate = user.Birthdate;
            UserId = user.UserId;
            Confirmed = (user.Confirmed == null) ? false : (bool) user.Confirmed;
            RegisterDate = (DateTime)user.RegisterDate;
        }

    }
}
