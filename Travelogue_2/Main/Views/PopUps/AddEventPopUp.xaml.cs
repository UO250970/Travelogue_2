using System;
using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEventPopUp : ContentPage
    {
        public AddToJourneyPopUpModel model;

        public AddEventPopUp()
        {
            InitializeComponent();

            BindingContext = model = new AddToJourneyPopUpModel();
        }

        private void CreateEvent(object sender, EventArgs e)
        {
            GridReservation.IsVisible = false;
            GridEvent.IsVisible = true;
        }

        private void CreateReservation(object sender, EventArgs e)
        {
            GridEvent.IsVisible = false;
            GridReservation.IsVisible = true;
        }
    }
}