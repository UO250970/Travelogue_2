using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
    public class SelectPopUpModel : DataBaseViewModel
    {

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

            PhotoTapped = new Command<ImageModel>(OnPhotoTapped);
        }

        public override void LoadData()
        {
            Photos.Clear();

            //DataBaseUtil.GetImagesFromJourney()?.ForEach(x => JourneysCreated.Add(x));
        }

        public void OnPhotoTapped(ImageModel image)
        {
            //return image;
        }

    }
}
