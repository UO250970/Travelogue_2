using Travelogue_2.Main.ViewModels.Library;
using Xamarin.Forms;

namespace Travelogue_2.Main.Views.Library
{
    public partial class ClosedJourneysView : ContentPage
    {

        readonly ClosedJourneysViewModel model;

        public ClosedJourneysView()
        {
            InitializeComponent();

            BindingContext = model = new ClosedJourneysViewModel();

            Shell.SetNavBarIsVisible(this, false);
        }
    }
}