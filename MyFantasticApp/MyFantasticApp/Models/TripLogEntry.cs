using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFantasticApp.Models
{
    public class TripLogEntry
    {
        [PrimaryKey, AutoIncrement]
        public int _id { get; set; }

        [Indexed]
        public string Title { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DateTime Date { get; set; }

        public int Rating { get; set; }

        public string Notes { get; set; }
    }
}
