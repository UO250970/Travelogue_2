using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCardPopUp : ContentPage
    {
        public AddSettingsPopUpModel model;

        public AddCardPopUp()
        {
            InitializeComponent();

            BindingContext = model = new AddSettingsPopUpModel();
        }
    }
}