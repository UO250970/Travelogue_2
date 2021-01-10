using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.Library;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library
{
    public class LibraryViewModel : BaseViewModel
    {
        public Command CreatedJourneysViewCommand { get; }
        public Command ClosedJourneysViewCommand { get; }


		public ObservableCollection<Item> Journeys { get; }
		public Command LoadJourneysCommand { get; }
		public Command<Item> JourneyTapped { get; }

		public LibraryViewModel()
        {
			CreatedJourneysViewCommand = new Command(() => CreatedJourneysViewC());
			ClosedJourneysViewCommand = new Command(() => ClosedJourneysViewC());

			LoadJourneysCommand = new Command(async () => await ExecuteLoadJourneysCommand());

			Journeys = new ObservableCollection<Item>();

			JourneyTapped = new Command<Item>(OnJourneySelected);
		}

		async Task ExecuteLoadJourneysCommand()
		{
			IsBusy = true;

			try
			{
				Journeys.Clear();
				var items = await DataStore.GetItemsAsync(true);
				foreach (var item in items)
				{
					Journeys.Add(item);
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


		public ObservableCollection<Item> CreatedJourneys
		{
			get => new ObservableCollection<Item>();
		}

		public ObservableCollection<Item> ClosedJourneys
		{
			get => new ObservableCollection<Item>();
		}


		async internal void CreatedJourneysViewC()
			=> await Shell.Current.GoToAsync(nameof(CreatedJourneysView));
		

		async internal void ClosedJourneysViewC()
			=> await Shell.Current.GoToAsync(nameof(ClosedJourneysView));
		

		async void OnJourneySelected(Item journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyDetailView)}?{nameof(ItemDetailViewModel.ItemId)}={journey.Id}");
		}

	}
}
