using MyFantasticApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFantasticApp.Services
{
    public class MockDataService
    {
        public MockDataService() { }
        public void InsertMockData()
        {
            DataService.Instance.SaveItem(new TripLogEntry
                {
                    _id = 0,
                    Title = "Washington Monument",
                    Notes = "Amazing!",
                    Rating = 3,
                    Date = new DateTime(2016, 10, 17),
                    Latitude = 38.8895,
                    Longitude = -77.0352
                });
            DataService.Instance.SaveItem(new TripLogEntry
                {
                    _id = 0,
                    Title = "Statue of Liberty",
                    Notes = "Inspiring!",
                    Rating = 4,
                    Date = new DateTime(2016, 10, 20),
                    Latitude = 40.6892,
                    Longitude = -74.0444
                });
            DataService.Instance.SaveItem(new TripLogEntry
                {
                    _id = 0,
                    Title = "Golden Gate Bridge",
                    Notes = "Foggy, but beautiful",
                    Rating = 5,
                    Date = new DateTime(2016, 10, 25),
                    Latitude = 37.8268,
                    Longitude = -122.4798
                });
        }
    }
}
