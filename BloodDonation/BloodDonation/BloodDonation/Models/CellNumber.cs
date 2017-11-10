using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Models
{
    class CellNumber
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public static int ID { get; set; }
        public static string FullName { get; set; }
        public static string Number { get; set; }
        public static string City { get; set; }
        public static string Area { get; set; }
        public static string BloodGroup { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string TodayDate { get; set; }
    }
}
