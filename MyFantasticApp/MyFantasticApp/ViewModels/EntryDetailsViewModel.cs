using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyFantasticApp.ViewModels
{
    class EntryDetailsViewModel : BaseViewModel
    {
        private Page _context;
        public string MainText = "Main text";

        public EntryDetailsViewModel(Page context)
        {
            _context = context;
        }
    }
}
