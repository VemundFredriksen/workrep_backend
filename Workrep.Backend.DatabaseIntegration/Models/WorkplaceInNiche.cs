using System;
using System.Collections.Generic;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class WorkplaceInNiche
    {
        public string NicheCode { get; set; }
        public long OrganizationNumber { get; set; }

        public virtual Niche NicheCodeNavigation { get; set; }
        public virtual Workplace OrganizationNumberNavigation { get; set; }
    }
}
