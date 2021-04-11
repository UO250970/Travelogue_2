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
            map.MoveToRegion(MapSpan.FromCenterAndRadius(model.GetPosition(), Distance.FromKilometers(10)));
            model.OnAppearing();
            base.OnAppearing();
        }

    }
}