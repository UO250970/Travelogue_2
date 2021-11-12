using Travelogue_2.Main.ViewModels.Library;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Library
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LibraryView : ContentPage
    {
        readonly LibraryViewModel model;

        public LibraryView()
        {
            InitializeComponent();

            BindingContext = model = new LibraryViewModel();

            Shell.SetNavBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            JourneisCreatedCollection.SelectedItem = null;
            JourneisClosedCollection.SelectedItem = null;
            model.OnAppearing();
        }
    }
}