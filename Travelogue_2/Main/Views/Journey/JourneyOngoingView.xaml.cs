using System.ComponentModel;
using Travelogue_2.Main.ViewModels.Journal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Journey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JourneyOngoingView : ContentPage
    {
		public JourneyOngoingView()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
        }

	}
}