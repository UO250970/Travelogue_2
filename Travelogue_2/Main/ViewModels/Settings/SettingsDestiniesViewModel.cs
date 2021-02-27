using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Models;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace Travelogue_2.Main.ViewModels.Settings
{
	public class SettingsDestiniesViewModel : BaseViewModel
	{
        public Command SearchDestinyCommand { get; }
        public Command AddDestinyCommand { get; }
        public ObservableCollection<DestinyCard> Destinies { get; }
        public ObservableCollection<DestinyCard> DestiniesSearched { get; }

        public SettingsDestiniesViewModel()
        {
            SearchDestinyCommand = new Command(() => SearchDestinyC());
            AddDestinyCommand = new Command(() => AddDestinyC());

            Destinies = new ObservableCollection<DestinyCard>();
            DestiniesSearched = new ObservableCollection<DestinyCard>();

            ExecuteLoadLanguagesCommand();
        }

        void ExecuteLoadLanguagesCommand()
        {
            IsBusy = true;

            try
            {
                Destinies.Clear();
                foreach (Destiny dest in CommonVariables.AvailableDestinies)
                {
                    DestinyCard temp = new DestinyCard();
                    temp.Destiny = dest.Name;
                    temp.Code = dest.Code;
                    temp.EmbassiesCities = dest.Embassies.Select(x => x.City).ToList();
                    temp.Embassies = new Dictionary<string, string>();
                    foreach( Embassy emb in dest.Embassies)
					{
                        temp.Embassies.Add(emb.City, emb.PhoneNumber);
					}

                    Destinies.Add(temp);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }

        public void OnAppearing()
            => IsBusy = true;

        public void SearchDestinyC()
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
                }
            }
        }

        public void AddDestinyC()
        {

        }
    }
}
