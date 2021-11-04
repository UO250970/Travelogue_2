using Travelogue_2.Main.ViewModels.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditOrDeleteEntryPopUp : ContentPage
    {
        public EditOrDeleteFromJourneyPopUpModel model;

        public EditOrDeleteEntryPopUp()
        {
            InitializeComponent();

            BindingContext = model = new EditOrDeleteFromJourneyPopUpModel();
        }
    }
}