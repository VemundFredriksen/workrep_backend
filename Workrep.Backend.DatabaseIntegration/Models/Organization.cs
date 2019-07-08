using System;
using System.Collections.Generic;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class Organization
    {
        public Organization()
        {
            OrganizationInNiche = new HashSet<OrganizationInNiche>();
            Workplace = new HashSet<Workplace>();
        }

        public long OrganizationNumber { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Homepage { get; set; }
        public string SectorCode { get; set; }
        public string Address { get; set; }
        public int? Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? Employees { get; set; }

        public virtual OrganizationBio OrganizationBio { get; set; }
        public virtual ICollection<OrganizationInNiche> OrganizationInNiche { get; set; }
        public virtual ICollection<Workplace> Workplace { get; set; }
    }
}
