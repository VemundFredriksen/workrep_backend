using System;
using System.Collections.Generic;

namespace Workrep.Backend.DatabaseIntegration.Models
{
    public partial class EmailConfirmation
    {
        public int UserId { get; set; }
        public string GenKey { get; set; }

        public virtual User User { get; set; }
    }
}
