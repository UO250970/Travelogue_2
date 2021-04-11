using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditOrDeleteToEntryPopUp : ContentPage
	{
		public EditOrDeleteToEntryPopUpModel model;
		
		public EditOrDeleteToEntryPopUp()
		{
			InitializeComponent();

			BindingContext = model = new EditOrDeleteToEntryPopUpModel();
		}
	}
}