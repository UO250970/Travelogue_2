using Travelogue_2.Main.ViewModels.Settings;
using Xamarin.Forms;

namespace Travelogue_2.Main.Views.Settings
{
    public partial class SettingsLanguageView : ContentPage
    {
        readonly SettingsLanguageViewModel model;

        public SettingsLanguageView()
        {
            InitializeComponent();

            BindingContext = model = new SettingsLanguageViewModel();

            Shell.SetNavBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            model.OnAppearing();
            base.OnAppearing();
        }
    }
}