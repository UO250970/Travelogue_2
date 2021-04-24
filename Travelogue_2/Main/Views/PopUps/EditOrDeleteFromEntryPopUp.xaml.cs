using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditOrDeleteFromEntryPopUp : ContentPage
	{
		public EditOrDeleteFromJourneyPopUpModel model;
		
		public EditOrDeleteFromEntryPopUp()
		{
			InitializeComponent();

			BindingContext = model = new EditOrDeleteFromJourneyPopUpModel();
		}
	}
}