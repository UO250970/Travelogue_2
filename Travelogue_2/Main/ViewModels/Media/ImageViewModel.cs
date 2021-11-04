using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Media
{
    [QueryProperty(nameof(ImageId), nameof(ImageId))]
    public class ImageViewModel : PhotoRendererModel
    {
        private string imageId;
        public string ImageId
        {
            get => imageId;
            set
            {
                SetProperty(ref imageId, value);
                LoadData();
            }
        }

        public override void LoadData()
        {
            if (ImageId != null)
            {
                ImageModel image = DataBaseUtil.GetImageById(int.Parse(ImageId));

                Caption = image.Caption;
                Journey = image.Journey;
                //CoverImage.ImageSour = journey.Image;

                // TODO - Chekear acciones según estado

                ImageSource = image.ImageSour;
            }
            //ImageName = "Prueba de imagen";
        }

        #region Journey
        private string journey;
        public string Journey
        {
            get => journey;
            set => SetProperty(ref journey, value);
        }
        #endregion

        #region Source
        private ImageSource imageSource;
        public ImageSource ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }
        #endregion

        #region Caption
        private string caption;
        public string Caption
        {
            get => caption;
            set => SetProperty(ref caption, value);
        }
        #endregion
    }
}
