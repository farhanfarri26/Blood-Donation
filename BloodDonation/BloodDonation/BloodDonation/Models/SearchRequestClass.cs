using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Models
{
    public class SearchRequestClass
    {
        public string City { get; set; }
        public string Hospitals { get; set; }
        public string BloodGroup { get; set; }
    }
}
