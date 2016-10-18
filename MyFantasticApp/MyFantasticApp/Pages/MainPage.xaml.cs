using MyFantasticApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MyFantasticApp.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Page title
            Title = "TripLog";

            var items = new List<TripLogEntry>
            {
                new TripLogEntry
                {
                    Title = "Washington Monument",
                    Notes = "Amazing!",
                    Rating = 3,
                    Date = new DateTime(2016, 10, 17),
                    Latitude = 38.8895,
                    Longitude = -77.0352
                }
            };

            // Create a new DataTemplate for type TextCell
            var itemTemplate = new DataTemplate(typeof(TextCell));
            itemTemplate.SetBinding(TextCell.TextProperty, "Title");
            itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");

            // Configure the ListView
            entryListView.ItemsSource = items;
            entryListView.ItemTemplate = itemTemplate;
        }
    }
}
