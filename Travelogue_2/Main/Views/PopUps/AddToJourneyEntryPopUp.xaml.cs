using Syncfusion.XForms.PopupLayout;
using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddToJourneyEntryPopUp : SfPopupLayout
	{
		public AddToJourneyEntryPopUpModel model;

		public AddToJourneyEntryPopUp()
		{
			InitializeComponent();

			BindingContext = model = new AddToJourneyEntryPopUpModel();

			Show();
		}
	}
}