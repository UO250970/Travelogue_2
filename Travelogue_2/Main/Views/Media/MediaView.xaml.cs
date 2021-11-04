using Syncfusion.XForms.TabView;
using System;
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
            base.OnAppearing();
        }

        protected override void OnAppearing()
        {
            model.OnAppearing();
        }

        private void TabView_TabItemTapped(object sender, TabItemTappedEventArgs e)
        {
            if (e.TabItem.Title.Equals(model.Resources["Map"]))
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(model.GetPosition(), Distance.FromKilometers(10)));
                model.GetMapPins().ForEach(x => map.Pins.Add(x));
            }
        }

        void JourneyTapped(object sender, EventArgs e)
        {
            base.OnAppearing();
        }

    }
}