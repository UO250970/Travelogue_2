using Travelogue_2.Main.ViewModels.Modelation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Modelation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModelationView : ContentPage
	{
		readonly ModelationViewModel model;

		public ModelationView()
		{
			InitializeComponent();

			BindingContext = model = new ModelationViewModel();

			Shell.SetNavBarIsVisible(this, false);
		}

		protected override void OnAppearing()
		{
			model.OnAppearing();
			base.OnAppearing();
		}
	}
}