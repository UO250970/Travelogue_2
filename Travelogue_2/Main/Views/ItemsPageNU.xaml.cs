using Travelogue_2.Main.ViewModels;
using Xamarin.Forms;

namespace Travelogue_2.Main.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModelNU _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModelNU();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}