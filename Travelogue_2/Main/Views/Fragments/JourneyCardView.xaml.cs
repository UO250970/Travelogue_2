using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Fragments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JourneyCardView : Frame
	{
		public new IGestureRecognizers GestureRecognizers;

		public JourneyCardView()
		{
			InitializeComponent();
		}

	}
}