using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workrep.Backend.API.Models.HttpModels
{
    public class ReviewSubmitBody
    {
        public int Rating { get; set; }
        public string Position { get; set; }
        public DateTime? EmploymentStart { get; set; }
        public DateTime? EmploymentEnd { get; set; }
        public string Comment { get; set; }
    }
}
