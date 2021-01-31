using Travelogue_2.Main.ViewModels.Modelation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Modelation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartModelationView : ContentPage
	{
		readonly StartModelationViewModel model;

		public StartModelationView()
		{
			InitializeComponent();

			BindingContext = model = new StartModelationViewModel();

			Shell.SetNavBarIsVisible(this, false);
		}
	}
}