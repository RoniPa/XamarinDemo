using MyFantasticApp.Pages;
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
            MainPage = new NavigationPage(new MainPage());
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
