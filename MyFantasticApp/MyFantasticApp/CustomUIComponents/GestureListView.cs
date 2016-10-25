using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyFantasticApp.CustomUIComponents
{
    public class GestureListView : ListView
    {
        public GestureListView() : base() { }

        public event EventHandler LongClicked;
        public void OnLongClicked()
        {
            LongClicked?.Invoke(this, new ItemTappedEventArgs(this, SelectedItem));
        }
    }
}
