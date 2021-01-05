using Travelogue_2.Main.ViewModels;
using Xamarin.Forms;

namespace Travelogue_2.Main.Views
{
    public partial class AboutPage : ContentPage
    {
        readonly AboutViewModel model;

        public AboutPage()
        {
            InitializeComponent();

            model = new AboutViewModel();
            BindingContext = model;

            Shell.SetNavBarIsVisible(this, false);
        }
    }
}