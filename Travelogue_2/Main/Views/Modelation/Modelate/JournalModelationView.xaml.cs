using Travelogue_2.Main.ViewModels.Modelation.Modelate;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Modelation.Modelate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JournalModelationView : ContentPage
	{
		public JournalModelationViewModel model;

		public JournalModelationView()
		{
			InitializeComponent();

			BindingContext = model = new JournalModelationViewModel();

			Shell.SetNavBarIsVisible(this, false);
		}
	}
}