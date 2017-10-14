using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonationWebApi.Models
{
    public class Donors
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CellNumber { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string BloodGroup { get; set; }
        public string Date { get; set; }

    }
}