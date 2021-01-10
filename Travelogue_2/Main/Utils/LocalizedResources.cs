﻿using System;
using System.Globalization;
using System.Resources;
using System.ComponentModel;
using Xamarin.Forms;

namespace Travelogue_2.Main.Utils
{
    public class LocalizedResources : INotifyPropertyChanged
    {
        const string DEFAULT_LANGUAGE = "";

        readonly ResourceManager ResourceManager;
        public CultureInfo CurrentCultureInfo;

        public string this[string key]
        {
            get => ResourceManager.GetString(key, CurrentCultureInfo);
        }

        public LocalizedResources(Type resource, string language = null)
            : this(resource, new CultureInfo(language ?? DEFAULT_LANGUAGE))
        { }

        public LocalizedResources(Type resource, CultureInfo cultureInfo)
        {
            CurrentCultureInfo = cultureInfo;
            ResourceManager = new ResourceManager(resource);

            MessagingCenter.Subscribe<object, CultureChangedMessage>(this,
                String.Empty, OnCultureChanged);
        }

        private void OnCultureChanged(object s, CultureChangedMessage ccm)
        {
            CurrentCultureInfo = ccm.NewCultureInfo;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentCulture() { return CurrentCultureInfo.Name; }
    }
}