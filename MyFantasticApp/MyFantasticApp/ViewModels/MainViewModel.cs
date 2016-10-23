using MyFantasticApp.Models;
using MyFantasticApp.Pages;
using MyFantasticApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public MainViewModel(INavService navService) : base(navService)
        {
            LogEntries = new ObservableCollection<TripLogEntry>();
        }

        public override async Task Init()
        {
            await LoadEntries();
        }
        #region Public Properties
        private ObservableCollection<TripLogEntry> _logEntries;
        public ObservableCollection<TripLogEntry> LogEntries
        {
            get { return _logEntries; }
            set {
                _logEntries = value;
                OnPropertyChanged("LogEntries");
            }
        }

        private TripLogEntry _selectedEntry = null;
        public TripLogEntry SelectedEntry
        {
            get
            {
                return _selectedEntry;
            }
            set
            {
                if (_selectedEntry != value)
                {
                    //_selectedEntry = value;
                    OnPropertyChanged("SelectedEntry");
                    ShowDetailsCommand.Execute(value);
                }
            }
        }
        #endregion Public Properties
        #region Commands
        /**
         * Navigate to new TripLogEntry Page
         */
        private Command _newEntryCommand;
        public Command NewEntryCommand {
            get {
                return _newEntryCommand ??
                    (_newEntryCommand = new Command(async () => {
                        await ExecuteNewEntryCommand(); }));
            }
        }
        async Task ExecuteNewEntryCommand()
        {
            await NavService.NavigateTo<NewEntryViewModel>();
        }
        /**
         * Navigate to TripLogEntry details Page
         */
        private Command<TripLogEntry> _showDetailsCommand;
        public Command<TripLogEntry> ShowDetailsCommand {
            get {
                return _showDetailsCommand ??
                    (_showDetailsCommand = new Command<TripLogEntry>(async (entry) => {
                        await ExecuteShowDetailsCommand(entry);
                    }));
            }
        }
        async Task ExecuteShowDetailsCommand(TripLogEntry entry)
        {
            await NavService.NavigateTo<EntryDetailsViewModel, TripLogEntry>(entry);
        }
        #endregion Commands
        #region Private Methods
        private async Task LoadEntries()
        {
            LogEntries.Clear();

            await Task.Factory.StartNew(() =>
            {
                LogEntries.Add(new TripLogEntry
                {
                    Title = "Washington Monument",
                    Notes = "Amazing!",
                    Rating = 3,
                    Date = new DateTime(2016, 10, 17),
                    Latitude = 38.8895,
                    Longitude = -77.0352
                });
                LogEntries.Add(new TripLogEntry
                {
                    Title = "Statue of Liberty",
                    Notes = "Inspiring!",
                    Rating = 4,
                    Date = new DateTime(2016, 10, 20),
                    Latitude = 40.6892,
                    Longitude = -74.0444
                });
                LogEntries.Add(new TripLogEntry
                {
                    Title = "Golden Gate Bridge",
                    Notes = "Foggy, but beautiful",
                    Rating = 5,
                    Date = new DateTime(2016, 10, 25),
                    Latitude = 37.8268,
                    Longitude = -122.4798
                });
            });
        }
        #endregion Private Methods
    }
}
