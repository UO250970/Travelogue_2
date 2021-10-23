using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation.Modelate
{
    public class BackgroundSelectorViewModel : PhotoRendererModel
    {
        public ObservableCollection<ImageModel> Backgrounds { get; set; }

        public Command NextCommand { get; set;  }
        public Command<ImageModel> BackgroundTapped { get; }

        public BackgroundSelectorViewModel()
        {
            Backgrounds = new ObservableCollection<ImageModel>();

            BackgroundTapped = new Command<ImageModel>(OnBackgrondSelected);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            Backgrounds = CommonVariables.GetBackgrounds();
        }

        async void OnBackgrondSelected(ImageModel background)
        {
            if (background == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(PageModelationView)}?{nameof(PageModelationViewModel.BackgroundId)}={background.ImageId}");
            // This will push the ItemDetailPage onto the navigation stack
        }
    }
}
