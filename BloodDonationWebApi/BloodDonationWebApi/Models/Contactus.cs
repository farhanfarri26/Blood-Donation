using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonationWebApi.Models
{
    public class Contactus
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string From { get; set; }
        public string CellNumber { get; set; }
    }
}