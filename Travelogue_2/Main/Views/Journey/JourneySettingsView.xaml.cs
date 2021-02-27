using Travelogue_2.Main.ViewModels.Journal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Journey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JourneySettingsView : ContentPage
	{
		public JourneySettingsViewModel model;

		public JourneySettingsView()
		{
			InitializeComponent();

			BindingContext = model = new JourneySettingsViewModel();
		}
	}
}