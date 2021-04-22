using Travelogue_2.Main.ViewModels;
using Xamarin.Forms;

namespace Travelogue_2.Main.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModelNU();
        }
    }
}