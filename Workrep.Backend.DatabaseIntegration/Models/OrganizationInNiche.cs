using System;
using System.Collections.Generic;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class OrganizationInNiche
    {
        public string NicheCode { get; set; }
        public long OrganizationNumber { get; set; }

        public virtual Niche NicheCodeNavigation { get; set; }
        public virtual Organization OrganizationNumberNavigation { get; set; }
    }
}
