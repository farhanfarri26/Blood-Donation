using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using Xamarin.Forms;

namespace BloodDonation.Models
{
    public class ProjectLocalDB
    {
        private SQLiteConnection _sqlconnection;
        public ProjectLocalDB()
        {
            _sqlconnection = DependencyService.Get<ISqlite>().GetConnection();
            _sqlconnection.CreateTable<CellNumber>();
        }

        //public IEnumerable<CellNumber> GetCellNumber()
        //{
        //    return (from c in _sqlconnection.Table<CellNumber>() select c).ToList();
        //}

        ////Get specific student  
        //public CellNumber GetCellNumber(int id)
        //{
        //    return _sqlconnection.Table<CellNumber>().FirstOrDefault(t=>(CellNumber.ID) == id);
        //}

        //public void AddCellNumber(CellNumber number)
        //{
        //    _sqlconnection.Insert(number);
        //}
    }
}
