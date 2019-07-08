using System;
using System.Collections.Generic;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class Workplace
    {
        public Workplace()
        {
            Review = new HashSet<Review>();
            WorkplaceInNiche = new HashSet<WorkplaceInNiche>();
        }

        public long OrganizationNumber { get; set; }
        public long? SuperOrganizationNumber { get; set; }
        public string Name { get; set; }
        public int? Employees { get; set; }
        public string Homepage { get; set; }
        public string Address { get; set; }
        public int? Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual Organization SuperOrganizationNumberNavigation { get; set; }
        public virtual WorkplaceBio WorkplaceBio { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<WorkplaceInNiche> WorkplaceInNiche { get; set; }
    }
}
