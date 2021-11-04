using Travelogue_2.Main.ViewModels.Journey;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Journey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JourneyView : ContentPage
    {
        public JourneyViewModel model;
        public JourneyView()
        {
            InitializeComponent();

            BindingContext = model = new JourneyViewModel();
        }

        protected override void OnAppearing()
        {
            model.OnAppearing();
            JourneySpace = new JourneyTemplateView();
        }

    }
}