using Travelogue_2.Main.ViewModels.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsCardHolderView : ContentPage
    {
        readonly SettingsCardHolderViewModel model;

        public SettingsCardHolderView()
        {
            InitializeComponent();

            BindingContext = model = new SettingsCardHolderViewModel();

            Shell.SetNavBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            model.OnAppearing();
            base.OnAppearing();
        }
    }
}