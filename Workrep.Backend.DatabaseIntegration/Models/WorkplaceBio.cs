using System;
using System.Collections.Generic;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class WorkplaceBio
    {
        public long OrganizationNumber { get; set; }
        public string Bio { get; set; }

        public virtual Workplace OrganizationNumberNavigation { get; set; }
    }
}
