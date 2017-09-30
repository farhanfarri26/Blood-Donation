using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonationWeb.Models
{
    public class DonateNow
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Hospitals { get; set; }
        public string BloodGroup { get; set; }
    }
}