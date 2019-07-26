using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workrep.Backend.API.Models
{
    public class ClientWorkplace
    {

        public long OrganizationNumber { get; set; }
        public long? SuperOrganizationNumber { get; set; }
        public string Name { get; set; }
        public int? EmployeeCount { get; set; }
        public string Homepage { get; set; }
        public string Address { get; set; }
        public int? ZIP { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string SuperName { get; set; }
        public int ReviewCount { get; set; }
        public float Rating { get; set; }
        public string Bio { get; set; }

    }
}
