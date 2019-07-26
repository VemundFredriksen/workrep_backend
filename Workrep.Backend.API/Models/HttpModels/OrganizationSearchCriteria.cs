using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workrep.Backend.API.Models
{
    public class OrganizationSearchCriteria
    {

        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public uint MinEmployees { get; set; }
        public uint MaxEmployees { get; set; }
        public ushort MinWorkplaces { get; set; }
        public uint MinReviewCount { get; set; }

    }
}
