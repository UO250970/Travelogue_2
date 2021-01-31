using Travelogue_2.Main.ViewModels.Modelation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Modelation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EndedModelationView : ContentPage
	{
		readonly EndedModelationViewModel model;

		public EndedModelationView()
		{
			InitializeComponent();

			BindingContext = model = new EndedModelationViewModel();

			Shell.SetNavBarIsVisible(this, false);
		}
	}
}