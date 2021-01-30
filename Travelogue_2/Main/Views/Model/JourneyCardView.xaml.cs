using Travelogue_2.Main.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Model
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JourneyCardView : ContentPage
	{
		public JourneyCardView(JourneyCard card)
		{
			InitializeComponent();

			//JourneyImage.Source = card.Image;
			//JourneyName.Text = card.Name;

		}
	}
}