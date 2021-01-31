using Travelogue_2.Main.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Fragments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JourneyBookView : Frame
	{
		public JourneyBookView()
		{
			InitializeComponent();
		}

		public JourneyBookView(JourneyCard card)
		{
			InitializeComponent();

			JourneyImage.Source = card.Image;
			JourneyName.Text = card.Name;
		}
	}
}