using Travelogue_2.Main.ViewModels.Journal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Journey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateEventView : ContentPage
	{
		public CreateEventViewModel model;

		public CreateEventView()
		{
			InitializeComponent();

			BindingContext = model = new CreateEventViewModel();
		}
	}
}