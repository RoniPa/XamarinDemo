using MyFantasticApp.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyFantasticApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private Page _context;
        public string MainText = "Main text";

        public MainViewModel(Page context)
        {
            _context = context;

            NewEntryCommand = new Command(() =>
            {
                _context.Navigation.PushAsync(new NewEntryPage());
            });
        }

        public Command NewEntryCommand { get; private set; }
    }
}
