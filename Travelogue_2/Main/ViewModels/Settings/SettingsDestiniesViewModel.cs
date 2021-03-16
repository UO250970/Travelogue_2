using System.Collections.ObjectModel;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Models.Cards;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using Travelogue_2.Main.Models;

namespace Travelogue_2.Main.ViewModels.Settings
{
	public class SettingsDestiniesViewModel : DataBaseViewModel
	{
        public Command SearchDestinyCommand { get; }
        public Command AddDestinyCommand { get; }
        public Dictionary<string,List<DestinyCard>> DestiniesOrdered { get; set; }
        public ObservableCollection<DestinyCard> Destinies { get; }
        public ObservableCollection<DestinyCard> DestiniesSearched { get; set; }

        public SettingsDestiniesViewModel()
        {
            SearchDestinyCommand = new Command(() => SearchDestinyC());
            AddDestinyCommand = new Command(() => AddDestinyC());

            DestiniesOrdered = new Dictionary<string, List<DestinyCard>>();
            Destinies = new ObservableCollection<DestinyCard>();
            DestiniesSearched = new ObservableCollection<DestinyCard>();

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            Destinies.Clear();
            foreach (Destiny dest in CommonVariables.AvailableDestinies)
            {
                DestinyCard temp = new DestinyCard();
                temp.Destiny = dest.Name;
                temp.Code = dest.Code;
                temp.EmbassiesCities = dest.Embassies.Select(x => x.City).ToList();
                temp.Embassies = new Dictionary<string, string>();
                foreach (Embassy emb in dest.Embassies)
                {
                    temp.Embassies.Add(emb.City, emb.PhoneNumber);
                }

                Destinies.Add(temp);
            }

            foreach (string s in CommonVariables.Alphabet)
            {
                var tempList = Destinies.Where(x => x.Destiny.ToUpper().StartsWith(s.ToUpper())).ToList();
                DestiniesOrdered.Add(s, tempList);
            }
        }

        async internal void SearchDestinyC()
		{
            SearchText = "";
            SearchVisible = !SearchVisible;
        }

        private bool searchVisible = false;

        public bool SearchVisible
        {
            get => searchVisible;
            set => SetProperty(ref searchVisible, value);
        }

        private string searchText = "";
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                var temp = Destinies.Where(x => x.Destiny.ToUpper().Contains(searchText.ToUpper()) == true);
                if (temp.Count() != DestiniesSearched.Count())
                {
                    DestiniesSearched.Clear();
                    foreach (var card in temp)
                    {
                        DestiniesSearched.Add(card);
                    }

                    DestiniesOrdered.Clear();
                    foreach (string s in CommonVariables.Alphabet)
                    {
                        var tempList = DestiniesSearched.Where(x => x.Destiny.ToUpper().StartsWith(s.ToUpper())).ToList();
                        DestiniesOrdered.Add(s, tempList);
                    }
                }
            }
        }

        public void AddDestinyC()
        {

        }

	}
}
