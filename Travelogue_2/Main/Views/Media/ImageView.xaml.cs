using Travelogue_2.Main.ViewModels.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Media
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageView : ContentPage
	{
		public ImageViewModel model;

		public ImageView()
		{
			InitializeComponent();

			BindingContext = model = new ImageViewModel();

			Shell.SetNavBarIsVisible(this, false);
		}
	}
}