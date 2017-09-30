using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonationWeb.Models
{
    public class FindDonor
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string BloodGroup { get; set; }
    }
}