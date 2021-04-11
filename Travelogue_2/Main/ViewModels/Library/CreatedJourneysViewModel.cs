using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.ViewModels.Journal;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.Library.Create;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library
{
	public class CreatedJourneysViewModel : DataBaseViewModel
    {

		public Command CreateJourneyCommand { get; }

		public Command SearchJourneyCommand { get; }
		public Command<JourneyCard> JourneyTapped { get; }
		public Command<JourneyCard> JourneyTappedDelete { get; }
		public ObservableCollection<JourneyCard> JourneysCreated { get; set; }
		public ObservableCollection<JourneyCard> JourneysCreatedSearched { get; set; }



		public CreatedJourneysViewModel()
		{

			CreateJourneyCommand = new Command(() => CreateJourneyC());
			SearchJourneyCommand = new Command(() => SearchJourneyC());

			JourneysCreated = new ObservableCollection<JourneyCard>();
			JourneysCreatedSearched = new ObservableCollection<JourneyCard>();

			JourneyTapped = new Command<JourneyCard>(OnJourneySelected);
			JourneyTappedDelete = new Command<JourneyCard>(OnJourneySelectedDelete);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			JourneysCreated.Clear();
			JourneysCreatedSearched.Clear();
			JourneyCard temp1 = new JourneyCard();
			temp1.Id = 0;
			temp1.Name = "Prueba";
			temp1.Image = ImageSource.FromResource(CommonVariables.GenericImage);


			JourneyCard temp2 = new JourneyCard();
			temp1.Id = 1;
			temp2.Name = "Prueba3";
			temp2.Image = ImageSource.FromResource(CommonVariables.GenericImage);

			JourneyCard temp3 = new JourneyCard();
			temp1.Id = 2;
			temp3.Name = "Prueba3";
			temp3.Image = ImageSource.FromResource(CommonVariables.GenericImage);

			JourneyCard temp4 = new JourneyCard();
			temp1.Id = 3;
			temp4.Name = "Prueba3";
			temp4.Image = ImageSource.FromResource(CommonVariables.GenericImage);

			JourneyCard temp5 = new JourneyCard();
			temp1.Id = 4;
			temp5.Name = "Prueba3";
			temp5.Image = ImageSource.FromResource(CommonVariables.GenericImage);

			JourneyCard temp6 = new JourneyCard();
			temp1.Id = 5;
			temp6.Name = "Prueba3";
			temp6.Image = ImageSource.FromResource(CommonVariables.GenericImage);

			JourneyCard temp7 = new JourneyCard();
			temp1.Id = 6;
			temp7.Name = "Prueba3";
			temp7.Image = ImageSource.FromResource(CommonVariables.GenericImage);

			JourneyCard temp8 = new JourneyCard();
			temp1.Id = 7;
			temp8.Name = "Prueba3";
			temp8.Image = ImageSource.FromResource(CommonVariables.GenericImage);

			JourneysCreated.Add(temp1);
			JourneysCreatedSearched.Add(temp1);
			JourneysCreated.Add(temp2);
			JourneysCreatedSearched.Add(temp2);
			JourneysCreated.Add(temp3);
			JourneysCreatedSearched.Add(temp3);
			JourneysCreated.Add(temp4);
			JourneysCreatedSearched.Add(temp4);
			JourneysCreated.Add(temp5);
			JourneysCreatedSearched.Add(temp5);
			JourneysCreated.Add(temp6);
			JourneysCreatedSearched.Add(temp6);
			JourneysCreated.Add(temp7);
			JourneysCreatedSearched.Add(temp7);
			JourneysCreated.Add(temp8);
			JourneysCreatedSearched.Add(temp8);

			//var items = await DataStore.GetItemsAsync(true);
			//foreach (var item in items)
			//{
			//Journeys.Add(item);
			//}
		}

		async internal void SearchJourneyC()
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
				var temp = JourneysCreated.Where(x => x.Name.ToUpper().Contains(searchText.ToUpper()) == true);
				if (temp.Count() != JourneysCreatedSearched.Count())
				{
					JourneysCreatedSearched.Clear();
					foreach (var card in temp)
					{
						JourneysCreatedSearched.Add(card);
					}
				}
			}
		}


		async internal void CreateJourneyC()
			=> await Shell.Current.GoToAsync(nameof(CreateJourneyView));

		async void OnJourneySelected(JourneyCard journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyTemplateViewModel.JourneyId)}={journey.Id}");
		}

		async void OnJourneySelectedDelete(JourneyCard journey)
		{
			if (journey == null)
				return;

			// TO-DO implementar
			bool result = await Alerter.AlertDeleteJourney();
			if (result)
			{
				JourneysCreated.Remove(journey);
				JourneysCreatedSearched.Remove(journey);
			}
		}

	}
}
