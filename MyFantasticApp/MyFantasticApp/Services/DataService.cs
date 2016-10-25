using MyFantasticApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyFantasticApp.Services
{
    public class DataService
    {
        private static object locker = new object();
        public static DataService Instance = new DataService();

        private SQLiteConnection _database;

        private DataService()
        {
            _database = DependencyService.Get<ISQLite>().GetConnection();
            _database.CreateTable<TripLogEntry>();
        }

        /**
         * Data manipulation & querying methods
         */
        public IEnumerable<TripLogEntry> GetItems()
        {
            lock(locker)
            {
                return (from i in _database.Table<TripLogEntry>() select i).ToList();
            }
        }
        public IEnumerable<TripLogEntry> GetItemsNotDone()
        {
            lock (locker)
            {
                return _database.Query<TripLogEntry>("SELECT * FROM [TripLogEntry] WHERE [Done] = 0");
            }
        }
        public TripLogEntry GetItem(int id)
        {
            lock (locker)
            {
                return _database.Table<TripLogEntry>().FirstOrDefault(x => x._id == id);
            }
        }
        public int SaveItem(TripLogEntry entry)
        {
            lock (locker)
            {
                if (entry._id != 0)
                {
                    _database.Update(entry);
                    return entry._id;
                }
                else
                {
                    return _database.Insert(entry);
                }
            }
        }
        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return _database.Delete<TripLogEntry>(id);
            }
        }

        public int DeleteItem(TripLogEntry entry)
        {
            if (entry._id != 0)
            {
                lock (locker)
                {
                    return _database.Delete<TripLogEntry>(entry._id);
                }
            }
            return entry._id;
        }
        public void ClearAllData()
        {
            lock (locker)
            {
                _database.Query<TripLogEntry>("DELETE FROM [TripLogEntry]");
            }
        }
    }
}
