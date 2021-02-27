using Travelogue_2.Main.ViewModels.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Settings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsDestiniesView : ContentPage
	{
		readonly SettingsDestiniesViewModel model;

		public SettingsDestiniesView()
		{
			InitializeComponent();

			BindingContext = model = new SettingsDestiniesViewModel();

			Shell.SetNavBarIsVisible(this, false);
		}

		protected override void OnAppearing()
		{
			model.OnAppearing();
			base.OnAppearing();
		}
	}
}