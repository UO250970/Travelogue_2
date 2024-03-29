﻿using Travelogue_2.Main.ViewModels.Library;
using Xamarin.Forms;

namespace Travelogue_2.Main.Views.Library
{
    public partial class CreatedJourneysView : ContentPage
    {

        readonly CreatedJourneysViewModel model;

        public CreatedJourneysView()
        {
            InitializeComponent();

            BindingContext = model = new CreatedJourneysViewModel();

            Shell.SetNavBarIsVisible(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            model.Back();
            return true;
        }

        protected override void OnAppearing()
        {
            CreatedJourneysCollection.SelectedItem = null;
            model.OnAppearing();
        }
    }
}