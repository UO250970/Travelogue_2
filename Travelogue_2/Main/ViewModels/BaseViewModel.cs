using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Travelogue_2.Resources.Localization;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public LocalizedResources Resources
        {
            get;
            private set;
        }
        public BaseViewModel()
        {
            Resources = new LocalizedResources(typeof(AppResources), App.CurrentLanguage);
        }

        /*public LocalizedResources Resources
        {
            get { return new LocalizedResources(typeof(AppResources), App.CurrentLanguage); }
            internal set { }
        }*/

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
