using Travelogue_2.Main.ViewModels.Journey;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Journey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JourneyOngoingView : ContentPage
    {
        public JourneyOngoingViewModel model;
		public JourneyOngoingView()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);

            BindingContext = model = new JourneyOngoingViewModel();
        }

        protected override void OnAppearing()
        {
            model.OnAppearing();
            //JourneyTemplateView view = 
            JourneySpace = new JourneyTemplateView();
            //JourneySpace.BindingContext = view.model;
        }

    }
}