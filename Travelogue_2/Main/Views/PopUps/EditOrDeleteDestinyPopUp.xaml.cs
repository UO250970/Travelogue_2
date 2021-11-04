using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditOrDeleteDestinyPopUp : ContentPage
    {
        public EditOrDeleteSettingsPopUpModel model;

        public EditOrDeleteDestinyPopUp()
        {
            InitializeComponent();

            BindingContext = model = new EditOrDeleteSettingsPopUpModel();
        }
    }
}