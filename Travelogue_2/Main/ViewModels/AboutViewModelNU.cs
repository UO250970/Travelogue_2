﻿using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels
{
    public class AboutViewModelNU : BaseViewModel
    {
        public AboutViewModelNU()
        {
            //Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}