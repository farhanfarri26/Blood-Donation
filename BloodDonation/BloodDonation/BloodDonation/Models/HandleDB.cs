using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BloodDonation.Models
{
    public class HandleDB
    {
        private SQLiteConnection _sqlconnection;

        public HandleDB()
        {
            _sqlconnection = DependencyService.Get<ISqlite>().GetConnection();
            _sqlconnection.CreateTable<LocalDB>();
        }

        public IEnumerable<LocalDB> GetDB()
        {
            return (from t in _sqlconnection.Table<LocalDB>() select t).ToList();
        }

       
        //Get specific student  
        public LocalDB GetDB(int id)
        {
            return _sqlconnection.Table<LocalDB>().FirstOrDefault(t => t.Id == id);
        }

        public void AddDB(LocalDB dB)
        {
            _sqlconnection.Insert(dB);
        }

        public void DeleteDB(LocalDB dB)
        {
            _sqlconnection.DeleteAll<LocalDB>();
        }

        public void UpdateDB(LocalDB dB)
        {
            _sqlconnection.Update(dB);
        }
    }
}
