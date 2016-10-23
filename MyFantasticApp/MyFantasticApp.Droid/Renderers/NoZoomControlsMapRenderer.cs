using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms;
using Android.Gms.Maps;
using System.ComponentModel;
using MyFantasticApp.Droid.Renderers;
using MyFantasticApp.CustomUIComponents;

[assembly: ExportRenderer(typeof(NoZoomControlsMap), typeof(NoZoomControlsMapRenderer))]
namespace MyFantasticApp.Droid.Renderers
{
    class NoZoomControlsMapRenderer : MapRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var map = ((MapView)Control).Map;
            map.UiSettings.ZoomControlsEnabled = false;
        }
    }
}