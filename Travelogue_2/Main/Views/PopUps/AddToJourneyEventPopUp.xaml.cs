using Syncfusion.XForms.PopupLayout;
using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddToJourneyEventPopUp : SfPopupLayout
	{
		public AddToJourneyEventPopUpModel model;

		public AddToJourneyEventPopUp()
		{
			InitializeComponent();

			BindingContext = model = new AddToJourneyEventPopUpModel();

			Show();
		}
	}
}