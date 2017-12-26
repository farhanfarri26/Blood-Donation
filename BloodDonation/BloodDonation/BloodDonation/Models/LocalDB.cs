using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Models
{
    public class LocalDB
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int _ID { get; set; }
        public string FullName { get; set; }
        public string CellNumber { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string BloodGroup { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TodayDate { get; set; }
        public string FutureUse { get; set; }
    }
}
