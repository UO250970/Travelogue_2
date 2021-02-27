using Syncfusion.XForms.PopupLayout;
using Travelogue_2.Main.ViewModels.Journal;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddToJourneyPopUp : SfPopupLayout
	{
		public JourneyOngoingViewModel model;

		public AddToJourneyPopUp(JourneyOngoingViewModel model)
		{
			InitializeComponent();

			BindingContext = this.model = model;

			Show();
		}
	}
}