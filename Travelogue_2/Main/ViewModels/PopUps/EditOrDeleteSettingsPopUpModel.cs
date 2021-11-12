using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;


namespace Travelogue_2.Main.ViewModels.PopUps
{
    [QueryProperty(nameof(CardId), nameof(CardId))]
    public class EditOrDeleteSettingsPopUpModel : PhotoRendererModel
    {
        public string cardId;
        public string CardId
        {
            get => cardId;
            set
            {
                cardId = value;
                LoadData();
            }
        }

        public Command AddTopImageCommand { get; }
        public Command AddBackImageCommand { get; }

        public Command SaveCardCommand { get; }
        public Command DeleteCardCommand { get; }

        public Command CancelCommand { get; }

        public EditOrDeleteSettingsPopUpModel()
        {
            AddTopImageCommand = new Command(() => AddTopImageC());
            AddBackImageCommand = new Command(() => AddBackImageC());

            SaveCardCommand = new Command(() => SaveCardC());
            DeleteCardCommand = new Command(() => DeleteCardC());

            CancelCommand = new Command(() => CancelC());
        }

        public override void LoadData()
        {
            if (CardId != null)
            {
                Card = DataBaseUtil.GetCardById(int.Parse(CardId));
            }
        }

        private CardModel card;
        public CardModel Card
        {
            get => card;
            set => SetProperty(ref card, value);
        }

        private Image topImage;


        private Image backImage;

        async internal void AddTopImageC()
        {
            ImageModel success = await DataBaseUtil.Photo(this, true);
            if (success != null)
            {
                Card.Top = success.Path;
            }
        }

        async internal void AddBackImageC()
        {
            ImageModel success = await DataBaseUtil.Photo(this, true);
            if (success != null)
            {
                Card.Back = success.Path;
            }
        }

        internal void SaveCardC()
        {
            DataBaseUtil.SaveCard(Card);
        }

        internal void DeleteCardC()
        {
            DataBaseUtil.DeleteCard(Card);
        }

        internal async void CancelC()
        {
            if (!Card.Name.Equals(string.Empty))
            {
                await Alerter.AlertNoNameInCard();
            }
            else if (!Card.Top.Equals(string.Empty) || !Card.Back.Equals(string.Empty))
            {
                await Alerter.AlertNoImagesInCard();
            } else
            {
                Back();
            }
        }
    }
}
