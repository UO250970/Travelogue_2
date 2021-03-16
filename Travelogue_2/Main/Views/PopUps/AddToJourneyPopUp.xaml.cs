using Syncfusion.XForms.PopupLayout;
using Travelogue_2.Main.ViewModels.Journal;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddToJourneyPopUp : SfPopupLayout
	{
		public JourneyTemplateViewModel model;

		public AddToJourneyPopUp(JourneyTemplateViewModel model)
		{
			InitializeComponent();

			BindingContext = this.model = model;

			Show();
		}
	}
}