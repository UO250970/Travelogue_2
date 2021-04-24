using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditOrDeleteCardPopUp : ContentPage
	{
		public EditOrDeleteSettingsPopUpModel model;

		public EditOrDeleteCardPopUp()
		{
			InitializeComponent();

			BindingContext = model = new EditOrDeleteSettingsPopUpModel();
		}
	}
}