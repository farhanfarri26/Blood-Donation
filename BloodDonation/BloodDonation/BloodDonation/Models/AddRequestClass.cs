﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Models
{
    class AddRequestClass
    {
        public string FullName { get; set; }
        public string CellNumber { get; set; }
        public string City { get; set; }
        public string Hospitals { get; set; }
        public string BloodGroup { get; set; }
    }
}
