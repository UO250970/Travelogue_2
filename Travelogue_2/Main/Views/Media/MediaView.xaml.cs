using Syncfusion.XForms.TabView;
using System;
using System.Threading.Tasks;
using Travelogue_2.Main.ViewModels.Media;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Travelogue_2.Main.Views.Media
{
    public partial class MediaView : ContentPage
    {

        readonly MediaViewModel model;

        public MediaView()
        {
            InitializeComponent();

            BindingContext = model = new MediaViewModel();

            Shell.SetNavBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            model.OnAppearing();
            new Task(StartMap).Start();
        }

        private async void StartMap()
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius(model.GetPosition(), Distance.FromKilometers(10)));
            model.GetMapPins().ForEach(x => map.Pins.Add(x));
        }

        void JourneyTapped(object sender, EventArgs e)
        {
            base.OnAppearing();
        }

    }
}