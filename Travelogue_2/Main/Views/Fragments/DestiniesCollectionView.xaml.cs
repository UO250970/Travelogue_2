using Travelogue_2.Main.ViewModels.Fragments;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Fragments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DestiniesCollectionView : Frame
	{
		public DestiniesCollectionView()
		{
			InitializeComponent();

			BindingContext = new DestiniesCollectionViewModel();
		}
	}
}