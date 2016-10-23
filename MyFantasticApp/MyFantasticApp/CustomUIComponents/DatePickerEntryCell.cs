using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyFantasticApp.CustomUIComponents
{
    public class DatePickerEntryCell : EntryCell
    {
        private static readonly BindableProperty DateProperty =
            BindableProperty
            .Create("DateProperty",
                typeof(DateTime), typeof(DateTime),
                DateTime.Now,
                propertyChanged: new BindableProperty
                    .BindingPropertyChangedDelegate(DatePropertyChanged));

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public new event EventHandler Completed;
        static void DatePropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            var @this = (DatePickerEntryCell)bindable;
            @this.Completed?.Invoke(bindable, new EventArgs());
        }
    }
}
