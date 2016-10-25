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
                    _selectedEntry = value;
                    OnPropertyChanged("SelectedEntry");
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
        private Command<TripLogEntry> _removeEntryCommand;
        public Command<TripLogEntry> RemoveEntryCommand {
            get {
                return _removeEntryCommand ??
                    (_removeEntryCommand = new Command<TripLogEntry>((entry) => {
                        DataService.Instance.DeleteItem(entry._id);
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            LogEntries.Remove(entry);
                            OnPropertyChanged("LogEntries");
                        });
                    }));
            }
        }
        #endregion Commands
        #region Private Methods
        private async Task LoadEntries()
        {
            await Task.Factory.StartNew(() =>
            {
                var items = DataService.Instance.GetItems();
                LogEntries.Clear();
                foreach (var item in items) LogEntries.Add(item);
                OnPropertyChanged("LogEntries");
            });
        }
        #endregion Private Methods
    }
}
