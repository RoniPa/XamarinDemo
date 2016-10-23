using MyFantasticApp.Models;
using MyFantasticApp.Services;
using MyFantasticApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            // Create a new DataTemplate for type TextCell
            var itemTemplate = new DataTemplate(typeof(TextCell));
            itemTemplate.SetBinding(TextCell.TextProperty, "Title");
            itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");
            
            entryListView.ItemTemplate = itemTemplate;

            BindingContext = new MainViewModel(DependencyService.Get<INavService>());
        }

        /**
         * A wrapper to cast View Model as MainViewModel
         */
        private MainViewModel _vm
            { get { return BindingContext as MainViewModel; } }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Manually init view model - needed because
            // page isn't navigated to through the navigation service
            if (_vm != null)
                await _vm.Init();
        }
    }
}
