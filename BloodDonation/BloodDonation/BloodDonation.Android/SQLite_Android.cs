using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BloodDonation.Droid;
using Xamarin.Forms;
using BloodDonation.Models;

[assembly: Dependency(typeof(SQLite_Android))]
namespace BloodDonation.Droid
{
    public class SQLite_Android : ISqlite
    {
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var filename = "Student.db3";
            var documentspath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);

            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);
            return connection;
        }
    }
}