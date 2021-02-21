using Syncfusion.XForms.PopupLayout;
using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddToJourneyPopUp : SfPopupLayout
	{
		public AddToJourneyPopUpModel model;

		public AddToJourneyPopUp()
		{
			InitializeComponent();

			BindingContext = model = new AddToJourneyPopUpModel();

			Show();
		}
	}
}