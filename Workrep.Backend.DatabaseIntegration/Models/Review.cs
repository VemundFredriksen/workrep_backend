using System;
using System.Collections.Generic;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int? UserId { get; set; }
        public long WorkplaceOrganizationNumber { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Position { get; set; }
        public DateTime? EmploymentStart { get; set; }
        public DateTime? EmploymentEnd { get; set; }
        public string Comment { get; set; }
        public int? Rating { get; set; }

        public virtual User User { get; set; }
        public virtual Workplace WorkplaceOrganizationNumberNavigation { get; set; }
    }
}
