using Travelogue_2.Main.ViewModels.Modelation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Modelation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContinueModelationView : ContentPage
    {
        readonly ContinueModelationViewModel model;

        public ContinueModelationView()
        {
            InitializeComponent();

            BindingContext = model = new ContinueModelationViewModel();

            Shell.SetNavBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            model.OnAppearing();
            ContinueJournalsCollection.SelectedItem = null;
            base.OnAppearing();
        }
    }
}