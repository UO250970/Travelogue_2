using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditOrDeleteEventPopUp : ContentPage
	{
		public EditOrDeleteFromJourneyPopUpModel model;

		public EditOrDeleteEventPopUp()
		{
			InitializeComponent();

			BindingContext = model = new EditOrDeleteFromJourneyPopUpModel();
		}
	}
}