using Travelogue_2.Main.ViewModels.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsStyleView : ContentPage
    {
        readonly SettingsStyleViewModel model;
        public SettingsStyleView()
        {
            InitializeComponent();

            BindingContext = model = new SettingsStyleViewModel();

            Shell.SetNavBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            model.OnAppearing();
            base.OnAppearing();
        }

    }
}