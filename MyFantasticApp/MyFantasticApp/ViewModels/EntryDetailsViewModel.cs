using MyFantasticApp.Models;
using MyFantasticApp.Pages;
using MyFantasticApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyFantasticApp.ViewModels
{
    class EntryDetailsViewModel : BaseViewModel<TripLogEntry>
    {
        public EntryDetailsViewModel(INavService navService) : base(navService) { }

        public override async Task Init(TripLogEntry entry)
        {
            PageTitle = entry.Title;
            Entry = entry;
        }
        #region Public Properties
        public string PageTitle { get; set; }

        private TripLogEntry _entry;
        public TripLogEntry Entry
        {
            get { return _entry; }
            set
            {
                if (_entry != value)
                {
                    _entry = value;
                    OnPropertyChanged("Entry");
                }
            }
        }
        #endregion Public Properties
    }
}
