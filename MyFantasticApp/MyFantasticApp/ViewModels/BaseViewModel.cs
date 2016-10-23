using MyFantasticApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFantasticApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavService NavService { get; private set; }

        protected BaseViewModel(INavService navService)
        {
            NavService = navService;
        }

        /**
         * Provides a way to initialize without
         * using the constructor. Used to refresh
         * ViewModels.
         */
        public abstract Task Init();

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?
                .Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class BaseViewModel<TParam> : BaseViewModel
    {
        protected BaseViewModel(INavService navService) : base(navService) { }
        /**
         * Provides a way to initialize without
         * using the constructor. Used to refresh
         * ViewModels.
         */
        public override async Task Init()
        {
            await Init(default(TParam));
        }

        public abstract Task Init(TParam param);
    }
}
