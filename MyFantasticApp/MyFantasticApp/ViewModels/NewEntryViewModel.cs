using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyFantasticApp.ViewModels
{
    class NewEntryViewModel : BaseViewModel
    {
        private Page _context;
        public string MainText = "Main text";

        public NewEntryViewModel(Page context)
        {
            _context = context;

            SaveEntryCommand = new Command(() =>
            {
                Debug.WriteLine("Save command");
            });
        }

        public Command SaveEntryCommand { get; private set; }
    }
}
