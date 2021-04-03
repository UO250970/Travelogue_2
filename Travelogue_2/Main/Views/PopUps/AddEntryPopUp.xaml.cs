using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEntryPopUp : ContentPage
	{
		public AddToJourneyPopUpModel model;

		public AddEntryPopUp()
		{
			InitializeComponent();

			BindingContext = model = new AddToJourneyPopUpModel();
		}
	}
}