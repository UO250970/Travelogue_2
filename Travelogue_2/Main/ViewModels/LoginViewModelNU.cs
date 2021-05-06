﻿using Travelogue_2.Main.Views;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels
{
    public class LoginViewModelNU : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModelNU()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}