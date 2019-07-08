using System;
using System.Collections.Generic;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class OrganizationBio
    {
        public long OrganizationNumber { get; set; }
        public string Bio { get; set; }

        public virtual Organization OrganizationNumberNavigation { get; set; }
    }
}
