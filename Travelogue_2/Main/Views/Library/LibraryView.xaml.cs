using Travelogue_2.Main.ViewModels.Library;
using Xamarin.Forms;

namespace Travelogue_2.Main.Views.Library
{
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
            base.OnAppearing();
            model.OnAppearing();
        }

    }
}