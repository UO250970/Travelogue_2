using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
    public class AddSettingsPopUpModel : PhotoRendererModel
    {
        public Command AddTopImageCommand { get; }
        public Command AddBackImageCommand { get; }
        public Command CreateCardCommand { get; }
        public Command CreateDestinyCommand { get; }
        public Command CancelCommand { get; }
        public Command<ImageModel> ImageTapped { get; }

        public AddSettingsPopUpModel()
        {
            AddTopImageCommand = new Command(() => AddTopImageC());
            AddBackImageCommand = new Command(() => AddBackImageC());
            CreateCardCommand = new Command(() => CreateCardC());
            CreateDestinyCommand = new Command(() => CreateDestinyC());
            CancelCommand = new Command(() => CancelC());

            ImageTapped = new Command<ImageModel>(OnImageSelected);
        }

        #region Title

        public string Name { get; set; } = string.Empty;

        #endregion

        #region CardImages

        public ImageModel topImage = new ImageModel();
        public ImageModel TopImage
        {
            get => topImage;
            set
            {
                SetProperty(ref topImage, value);
            }
        }

        public ImageModel backImage = new ImageModel();
        public ImageModel BackImage
        {
            get => backImage;
            set
            {
                SetProperty(ref backImage, value);
            }
        }

        #endregion

        public override void LoadData()
        {
            
        }

        async internal void AddTopImageC()
        {
            ImageModel success = await DataBaseUtil.Photo(this, true);
            if (success != null)
            {
                TopImage = success;
            }
        }

        async internal void AddBackImageC()
        {
            ImageModel success = await DataBaseUtil.Photo(this, true);
            if (success != null)
            {
                BackImage = success;
            }
        }

        async void CreateCardC()
        {
            if (Name == string.Empty)
            {
                await Alerter.AlertNoNameInCard();
            }
            else if (TopImage.Path.Equals(string.Empty) || BackImage.Path.Equals(string.Empty))
            {
                await Alerter.AlertNoImagesInCard();
            }
            else
            {
                DataBaseUtil.CreateCard(Name, TopImage, BackImage);
                await Alerter.AlertCardCreated();
                Back();
            }
        }

        public void CreateDestinyC()
        {

        }

        public async Task CancelC()
        {
            Back();
        }

        async void OnImageSelected(ImageModel image)
        {
            if (image == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
        }

    }
}
