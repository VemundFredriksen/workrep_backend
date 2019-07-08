using System;
using System.Collections.Generic;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class Niche
    {
        public Niche()
        {
            OrganizationInNiche = new HashSet<OrganizationInNiche>();
            WorkplaceInNiche = new HashSet<WorkplaceInNiche>();
        }

        public string NicheCode { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OrganizationInNiche> OrganizationInNiche { get; set; }
        public virtual ICollection<WorkplaceInNiche> WorkplaceInNiche { get; set; }
    }
}
