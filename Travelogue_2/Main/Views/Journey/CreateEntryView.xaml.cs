using Travelogue_2.Main.ViewModels.Journal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Journey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateEntryView : ContentPage
	{
		public CreateEntryViewModel model; 

		public CreateEntryView()
		{
			InitializeComponent();

			BindingContext = model = new CreateEntryViewModel();
		}
	}
}