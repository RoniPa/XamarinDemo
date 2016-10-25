using MyFantasticApp.Converters;
using MyFantasticApp.CustomUIComponents;
using MyFantasticApp.Models;
using MyFantasticApp.Services;
using MyFantasticApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyFantasticApp.Pages
{
    public partial class EntryDetailsPage : ContentPage
    {
        readonly NoZoomControlsMap _map;

        public EntryDetailsPage()
        {
            BindingContextChanged += (sender, args) =>
            {
                if (_vm == null) return;

                _vm.PropertyChanged += (s, e) => {
                    if (e.PropertyName == "Entry")
                        UpdateMap();
                };
            };

            this.SetBinding(TitleProperty, "Entry.Title");

            _map = new NoZoomControlsMap { 
                IsShowingUser = true,
                MapType = MapType.Hybrid
            };

            InitializeComponent();
            
            BindingContext = new EntryDetailsViewModel(DependencyService.Get<INavService>());

            MainGrid.Children.Add(_map);
            Grid.SetRowSpan(_map, 3);

            _displayDetails();
        }

        /**
         * A private reference to the view model -
         * needed because map doesn't have bindable properties
         */
        private EntryDetailsViewModel _vm
        {
            get { return BindingContext as EntryDetailsViewModel; }
        }

        /**
         * Update map data when view model is initialized
         */
        private void UpdateMap()
        {
            if (_vm.Entry == null)
                return;

            _map.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(_vm.Entry.Latitude, _vm.Entry.Longitude),
                    Distance.FromKilometers(.5)));

            _map.Pins.Add(new Pin {
                Type = PinType.Place,
                Label = _vm.Entry.Title,
                Position = new Position (_vm.Entry.Latitude, _vm.Entry.Longitude)
            });
        }

        /**
         * Create details view to page
         */
        private void _displayDetails()
        {
            var title = new Label
                { HorizontalOptions = LayoutOptions.Center };
            title.SetBinding(Label.TextProperty, "Entry.Title");

            var date = new Label
                { HorizontalOptions = LayoutOptions.Center };
            date.SetBinding(Label.TextProperty, "Entry.Date", stringFormat:"{0:M}");

            var rating = new Image
                { HorizontalOptions = LayoutOptions.Center };
            rating.SetBinding(Image.SourceProperty, "Entry.Rating", converter:new RatingToStarConverter());

            var notes = new Label
                { HorizontalOptions = LayoutOptions.Center };
            notes.SetBinding(Label.TextProperty, "Entry.Notes");

            var details = new StackLayout {
                Padding = 10,
                Children = {
                    title, date, rating, notes
                }
            };

            var detailsBg = new BoxView {
                BackgroundColor = Color.White,
                Opacity = .8
            };

            MainGrid.Children.Add(detailsBg, 0, 1);
            MainGrid.Children.Add(details, 0, 1);
        }
    }
}
