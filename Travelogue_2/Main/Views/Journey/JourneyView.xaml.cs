using Travelogue_2.Main.ViewModels.Journal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Journey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JourneyView : ContentPage
	{

		readonly JourneyViewModel model;

		public JourneyView()
		{
			InitializeComponent();

			BindingContext = model = new JourneyViewModel();
		}

		protected override void OnAppearing()
		{
			model.OnAppearing();
			base.OnAppearing();
		}
	}
}