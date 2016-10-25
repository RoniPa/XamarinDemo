using MyFantasticApp.Models;
using MyFantasticApp.Services;
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
        readonly ILocationService _locService;

        public NewEntryViewModel(INavService navService, ILocationService locService) : base(navService)
        {
            _locService = locService;

            EntryDate = DateTime.Today;
            EntryRating = 1;
        }

        public override async Task Init()
        {
            var coords = await
                _locService.GetGeoCoordinatesAsync();

            EntryLatitude = coords.Latitude;
            EntryLongitude = coords.Longitude;
        }

        private string _entryTitle;
        public string EntryTitle {
            get { return _entryTitle; }
            set {
                _entryTitle = value;
                OnPropertyChanged("EntryTitle");
                /**
                 * Must be called in all property setters
                 * that affect the result of the CanExecute function.
                 */
                SaveEntryCommand.ChangeCanExecute();
            }
        }
        #region Public Properties
        public double EntryLatitude { get; set; }
        public double EntryLongitude { get; set; }
        public DateTime EntryDate { get; set; }
        public int EntryRating { get; set; }
        public string EntryNotes { get; set; }
        #endregion Public Properties
        #region Commands
        private Command _saveEntryCommand;
        public Command SaveEntryCommand {
            get {
                return _saveEntryCommand ?? (_saveEntryCommand =
                    new Command(async () => {
                        await ExecuteSaveCommand();
                    }, CanSave));
            }
        }
        private async Task ExecuteSaveCommand()
        {
            var newEntry = new TripLogEntry
            {
                _id = 0,
                Title = EntryTitle,
                Latitude = EntryLatitude,
                Longitude = EntryLongitude,
                Date = EntryDate,
                Rating = EntryRating,
                Notes = EntryNotes
            };

            DataService.Instance.SaveItem(newEntry);
            await NavService.GoBack();
        }
        #endregion Commands

        bool CanSave() { return !string.IsNullOrWhiteSpace(EntryTitle); }
    }
}
