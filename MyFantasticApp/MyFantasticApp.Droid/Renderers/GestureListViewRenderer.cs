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
using Xamarin.Forms.Platform.Android;
using MyFantasticApp.Droid.Renderers;
using Xamarin.Forms;
using MyFantasticApp.CustomUIComponents;
using static Android.Widget.AdapterView;

[assembly: ExportRenderer(typeof(GestureListView), typeof(GestureListViewRenderer))]
namespace MyFantasticApp.Droid.Renderers
{
    class GestureListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var view = (GestureListView)e.NewElement;
                Control.ItemLongClick += (s, args) => view.OnLongClicked();
            }
        }
    }
}