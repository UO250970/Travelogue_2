using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
    public class SelectPopUpModel : ModelationSelectorViewModel
    {

        public Command CancelCommand { get; }
        public Command ContinueCommand { get; }

        public Command<ImageModel> PhotoTapped { get; }

        private ObservableCollection<ImageModel> photos;
        public ObservableCollection<ImageModel> Photos
        {
            get => photos;
            set => SetProperty(ref photos, value);
        }

        public SelectPopUpModel()
        {
            Photos = new ObservableCollection<ImageModel>();

            ContinueCommand = new Command(() => ContinueC());
            CancelCommand = new Command(() => CancelC());
            PhotoTapped = new Command<ImageModel>(OnPhotoTapped);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            Photos.Clear();

            DataBaseUtil.GetImagesFromJournalId(int.Parse(CurrentJourneyId))?.ForEach(x => Photos.Add(x));
        }

        public void ContinueC()
        {
            Back();
        }

        public void CancelC()
        {
            ImageSelectedPath = string.Empty;
            Back();
        }

        public void OnPhotoTapped(ImageModel image)
        {
            ImageSelectedPath = image.Path;
        }

    }
}
