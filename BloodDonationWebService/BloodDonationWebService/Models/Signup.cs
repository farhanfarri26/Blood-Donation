using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonationWebService.Models
{
    public class Signup
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CellNumber { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string BloodGroup { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}