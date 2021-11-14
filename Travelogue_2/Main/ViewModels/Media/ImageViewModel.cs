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

        public Command SaveImageCommand { get; }
        public Command ShareImageCommand { get; }
        public Command DeleteImageCommand { get; }

        public ImageViewModel()
        {
            SaveImageCommand = new Command(() => SaveImageC());
            ShareImageCommand = new Command(() => ShareImageC());
            DeleteImageCommand = new Command(() => DeleteImageC());
        }

        public override void LoadData()
        {
            if (ImageId != null)
            {
                ImageModel image = DataBaseUtil.GetImageById(int.Parse(ImageId));

                Caption = image.Caption;
                Journey = image.Journey;
                ImageSource = image.ImageSour;
            }
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

        #region Commands

        internal async void SaveImageC()
        {
            ImageModel model = DataBaseUtil.GetImageById( int.Parse(ImageId));
            model.Caption = caption;

            if (DataBaseUtil.SaveImage(model)) await Alerter.AlertImageSaved();
            Back();
        }

        internal void ShareImageC()
        {
            DataBaseUtil.ShareImage(int.Parse(ImageId));
        }

        internal async void DeleteImageC()
        {
            bool result = await Alerter.AlertImageWillBeDeleted();

            if (result)
            {
                DataBaseUtil.DeleteImageById(int.Parse(ImageId));
                Back();
            }
        }
        #endregion
    }
}
