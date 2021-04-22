using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddDestinyPopUp : ContentPage
	{
		public AddSettingsPopUpModel model;

		public AddDestinyPopUp()
		{
			InitializeComponent();

			BindingContext = model = new AddSettingsPopUpModel();
		}
	}
}