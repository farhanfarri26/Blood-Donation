using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebApi.Models
{
    public class AddRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CellNumber { get; set; }
        public string City { get; set; }
        public string Hospitals { get; set; }
        public string BloodGroup { get; set; }
    }
}