using System.Collections.ObjectModel;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Models;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using Travelogue_2.Main.Views.PopUps;
using Xamarin.Essentials;
using Travelogue_2.Main.BBDD;

namespace Travelogue_2.Main.ViewModels.Settings
{
	public class SettingsDestiniesViewModel : DataBaseViewModel
	{
        public Command SearchDestinyCommand { get; }
        public Command AddDestinyCommand { get; }
        public Command MoreInfoCommand { get; }
        public Command PhoneNumberTappedCommand { get; }

        public Command<DestinyModel> DestinyTapped { get; }
        public ObservableDictionary<string,List<DestinyModel>> DestiniesOrdered { get; set; }
        public ObservableCollection<DestinyModel> Destinies { get; }
        public ObservableCollection<DestinyModel> DestiniesSearched { get; set; }

        public SettingsDestiniesViewModel()
        {
            SearchDestinyCommand = new Command(() => SearchDestinyC());
            AddDestinyCommand = new Command(() => AddDestinyCAsync());
            MoreInfoCommand = new Command<string>((x) => MoreInfoC(x));
            PhoneNumberTappedCommand = new Command<string>((x) => PhoneNumberTappedC(x));

            DestiniesOrdered = new ObservableDictionary<string, List<DestinyModel>>();
            Destinies = new ObservableCollection<DestinyModel>();
            DestiniesSearched = new ObservableCollection<DestinyModel>();

            DestinyTapped = new Command<DestinyModel>(OnDestinySelected);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
            Destinies.Clear();
            DestiniesOrdered.Clear();
            DestiniesSearched.Clear();
            CommonVariables.AvailableDestinies.ForEach(x => Destinies.Add(x) );

            foreach (string s in CommonVariables.Alphabet)
            {
                var tempList = Destinies.Where(x => x.Destiny.ToUpper().StartsWith(s.ToUpper())).ToList();
                DestiniesOrdered.Add(s, tempList);
            }
        }

		#region search
		internal void SearchDestinyC()
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
                        if (tempList.Count != 0)
						{
                            DestiniesOrdered.Add(s, tempList);
                        }
                    }
                }
            }
        }
        #endregion

        public async void AddDestinyCAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddDestinyPopUp)}");
        }

        async internal void MoreInfoC(string path)
        {
            await Browser.OpenAsync(path);
        }

        internal void PhoneNumberTappedC(string number)
        {
            PhoneDialer.Open(number);

            //notificationManager.SendNotification("Title", "Message");
        }

        async void OnDestinySelected(DestinyModel destiny)
        {
            if (destiny == null)
                return;

            // TODO -  A futuros, abrir pestañita con info de forma chula
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
        }
    }
}
