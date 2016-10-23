using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFantasticApp.ViewModels;
using Xamarin.Forms;
using System.Reflection;
using MyFantasticApp.Services;

[assembly: Dependency(typeof(FormsNavigationService))]
namespace MyFantasticApp.Services
{
    class FormsNavigationService : INavService
    {
        public INavigation XamarinFormsNav { get; set; }
        /**
         * Maps the ViewModels to Pages
         */
        readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public void RegisterViewMapping (Type viewModel, Type view)
        {
            _map.Add(viewModel, view);
        }

        #region Interface Implementation
        public bool CanGoBack
        {
            get
            {
                return XamarinFormsNav.NavigationStack != null
                    && XamarinFormsNav.NavigationStack.Count > 0;
            }
        }

        public event PropertyChangedEventHandler CanGoBackChanged;
        void OnCanGoBackChanged()
        {
            CanGoBackChanged?.Invoke(this, new PropertyChangedEventArgs("CanGoBack"));
        }

        public async Task ClearBackStack()
        {
            if (XamarinFormsNav.NavigationStack.Count <= 1)
                return;

            for (var i = 0; i < XamarinFormsNav.NavigationStack.Count - 1; i++) {
                XamarinFormsNav.RemovePage(XamarinFormsNav.NavigationStack[i]);
            }
        }

        public async Task GoBack()
        {
            if (CanGoBack) {
                await XamarinFormsNav.PopAsync(true);
            }

            OnCanGoBackChanged();
        }

        public async Task NavigateTo<TVM>() where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));

            var vm = XamarinFormsNav.NavigationStack.Last().BindingContext;
            if (vm is BaseViewModel) {
                await ((BaseViewModel)vm).Init();
            }
        }

        public async Task NavigateTo<TVM, TParam>(TParam param) where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));

            var vm = XamarinFormsNav.NavigationStack.Last().BindingContext;
            if (vm is BaseViewModel<TParam>)
            {
                await((BaseViewModel<TParam>)vm).Init(param);
            }
        }

        private async Task NavigateToView(Type viewModelType)
        {
            Type viewType;

            if (!_map.TryGetValue(viewModelType, out viewType))
                throw new ArgumentException("No view found in View Mapping for " + viewModelType.FullName + ".");

            var constructor = viewType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(dc => dc.GetParameters().Count() <= 0);
            var view = constructor.Invoke(null) as Page;
            await XamarinFormsNav.PushAsync(view, true);
        }

        public async Task NavigateToUri(Uri uri)
        {
            if (uri == null)
                throw new ArgumentException("Invalid URI");

            Device.OpenUri(uri);
        }

        public async Task RemoveLastView()
        {
            if (XamarinFormsNav.NavigationStack.Any())
            {
                var lastView = XamarinFormsNav.NavigationStack[XamarinFormsNav.NavigationStack.Count - 1];
                XamarinFormsNav.RemovePage(lastView);
            }
        }
        #endregion Interface Implementation
    }
}
