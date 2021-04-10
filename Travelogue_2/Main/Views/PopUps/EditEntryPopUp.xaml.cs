using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditEntryPopUp : ContentPage
	{
		public AddToJourneyPopUpModel model;

		public EditEntryPopUp()
		{
			InitializeComponent();

			BindingContext = model = new AddToJourneyPopUpModel();
		}
	}
}