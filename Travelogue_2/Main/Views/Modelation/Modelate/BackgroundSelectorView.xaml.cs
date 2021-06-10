using Travelogue_2.Main.ViewModels.Modelation.Modelate;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Modelation.Modelate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BackgroundSelectorView : ContentPage
	{
		public BackgroundSelectorViewModel model;

		public BackgroundSelectorView ()
		{
			InitializeComponent ();

			BindingContext = model = new BackgroundSelectorViewModel();
		}
	}
}