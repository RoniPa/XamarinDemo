using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using MyFantasticApp.Droid.Services;
using MyFantasticApp.Services;
using MyFantasticApp.Models;
using System.Threading.Tasks;
using Plugin.Geolocator;
using System;

[assembly: Dependency(typeof(LocationService))]
namespace MyFantasticApp.Droid.Services
{
    class LocationService : ILocationService
    {
        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 30;

                var position = await locator.GetPositionAsync(30000);

                return new GeoCoords
                {
                    Latitude = position.Latitude,
                    Longitude = position.Longitude
                };
            } catch (Exception ex) {
                System.Diagnostics.Debug
                    .WriteLine("Unable to get location, may need to increase timeout: " + ex);

                return new GeoCoords
                {
                    Latitude = 0,
                    Longitude = 0
                };
            }
        }
    }
}