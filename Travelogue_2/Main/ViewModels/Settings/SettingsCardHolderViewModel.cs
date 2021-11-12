using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.ViewModels.PopUps;
using Travelogue_2.Main.Views.PopUps;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Settings
{
    public class SettingsCardHolderViewModel : PhotoRendererModel
    {
        public Command SearchCardCommand { get; }
        public Command AddCardCommand { get; }

        private ObservableCollection<CardModel> cards;
        public ObservableCollection<CardModel> Cards
        {
            get => cards;
            set => SetProperty(ref cards, value);
        }
        public ObservableCollection<CardModel> CardsSearched { get; set; }
        public Command<CardModel> EditOrDeleteCardCommand { get; }

        public SettingsCardHolderViewModel()
        {
            SearchCardCommand = new Command(() => SearchCardC());
            AddCardCommand = new Command(() => AddCardC());

            Cards = new ObservableCollection<CardModel>();
            CardsSearched = new ObservableCollection<CardModel>(); 
            EditOrDeleteCardCommand = new Command<CardModel>((CardModel e) => EditOrDeleteCardC(e));

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            Cards.Clear();

            DataBaseUtil.GetCards().ForEach(x => Cards.Add(x));
        }

        async internal void SearchCardC()
        {
            SearchText = string.Empty;
            SearchVisible = !SearchVisible;
        }

        private bool searchVisible = false;

        public bool SearchVisible
        {
            get => searchVisible;
            set => SetProperty(ref searchVisible, value);
        }

        private string searchText = string.Empty;
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                var temp = Cards.Where(x => x.Name.ToUpper().Contains(searchText.ToUpper()) == true);
                if (temp.Count() != CardsSearched.Count())
                {
                    CardsSearched.Clear();
                    foreach (var card in temp)
                    {
                        CardsSearched.Add(card);
                    }
                }
            }
        }

        public async void AddCardC()
        {
            await Shell.Current.GoToAsync($"{nameof(AddCardPopUp)}");
        }

        async internal void EditOrDeleteCardC(CardModel card)
        {
            await Shell.Current.GoToAsync($"{nameof(EditOrDeleteCardPopUp)}?{nameof(EditOrDeleteSettingsPopUpModel.CardId)}={card.Id}");
        }

    }
}
