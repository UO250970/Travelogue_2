using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation.Modelate
{
    [QueryProperty("PageNum", "PageNum")]
    public class BackgroundSelectorViewModel : PhotoRendererModel
    {
        private ObservableCollection<ImageModel> backgrounds;
        public ObservableCollection<ImageModel> Backgrounds
        {
            get => backgrounds;
            set => SetProperty(ref backgrounds, value);
        }

        private string pageNum;
        public string PageNum
        {
            get => pageNum;
            set => pageNum = value;
        }

        public Command NextCommand { get; set; }
        public Command<ImageModel> BackgroundTapped { get; }

        public BackgroundSelectorViewModel()
        {
            Backgrounds = new ObservableCollection<ImageModel>();

            BackgroundTapped = new Command<ImageModel>(OnBackgrondSelected);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            Backgrounds.Clear();
            CommonVariables.GetBackgrounds().ToList().ForEach(x => Backgrounds.Add(x));
            //DataBaseUtil.GetImages().ToList().ForEach(x => Backgrounds.Add(x));
        }

        async void OnBackgrondSelected(ImageModel background)
        {
            if (background == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(PageModelationView)}?{nameof(PageModelationViewModel.BackgroundPath)}={background.Path}&" +
                                                                    $"{nameof(PageModelationViewModel.PageNum)}={PageNum}&" +
                                                                    $"{nameof(PageModelationViewModel.NewPage)}={CommonVariables.True}");
        }

    }
}
