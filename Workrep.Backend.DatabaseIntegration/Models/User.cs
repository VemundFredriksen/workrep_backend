using System;
using System.Collections.Generic;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class User
    {
        public User()
        {
            Review = new HashSet<Review>();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public int UserId { get; set; }
        public bool? Confirmed { get; set; }
        public DateTime? RegisterDate { get; set; }

        public virtual EmailConfirmation EmailConfirmation { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
