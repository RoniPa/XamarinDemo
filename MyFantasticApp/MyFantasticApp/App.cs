using MyFantasticApp.Pages;
using MyFantasticApp.Services;
using MyFantasticApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MyFantasticApp
{
    public class App : Application
    {
        public App()
        {
            /**
             * The root page of the application.
             * The NavigationPage element wrapping
             * the MainPage enables navigating between
             * pages.
             */
            var mainPage = new NavigationPage(new MainPage());

            var navService = DependencyService.Get<INavService>() as FormsNavigationService;
            navService.XamarinFormsNav = mainPage.Navigation;
            navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navService.RegisterViewMapping(typeof(EntryDetailsViewModel), typeof(EntryDetailsPage));
            navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
