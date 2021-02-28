using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Models.Entries;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.PopUps;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journal
{
	public class JourneyOngoingViewModel : PhotoRendererModel
	{
		private string JourneyId { get; set; }
		public Command AddImageCommand { get; }
		public Command<ImageCard> ImageTapped { get; }
		public Command<DayCard> DayTapped { get; }
		public ObservableCollection<ImageCard> JourneyImages { get; }
		public ObservableCollection<DayCard> JourneyDays { get; }
		public ObservableCollection<EventCard> JourneyEvents { get; }
		public ObservableCollection<EntryCard> JourneyEntries { get; }

		/** Pop Ups */
		public Command ModifyJourneyCommand { get; }
		public Command AddToJourneyCommand { get; }
		public Command CreateEventCommand { get; }
		public Command CreateEntryCommand { get; }
		/** */

		public JourneyOngoingViewModel()
		{
			AddImageCommand = new Command(() => AddImageC());
			ModifyJourneyCommand = new Command(() => ModifyJourneyC());
			AddToJourneyCommand = new Command(() => AddToJourneyC());
			CreateEventCommand = new Command(() => CreateEventC());
			CreateEntryCommand = new Command(() => CreateEntryC());

			JourneyImages = new ObservableCollection<ImageCard>();
			JourneyDays = new ObservableCollection<DayCard>();
			JourneyDays.CollectionChanged += JourneyDaysChanged;
			JourneyEvents = new ObservableCollection<EventCard>();
			JourneyEntries = new ObservableCollection<EntryCard>();

			ImageTapped = new Command<ImageCard>(OnImageSelected);
			DayTapped = new Command<DayCard>(OnDaySelected);

			ExecuteLoadDataCommand();
		}

		async Task ExecuteLoadDataCommand()
		{
			IsBusy = true;

			try
			{
				JourneyId = "1";
				var temp = new DayCard();
				temp.Day = "2";
				temp.Month = "2";
				JourneyDays.Add(temp);

				var temp2 = new DayCard();
				temp2.Day = "3";
				temp2.Month = "2";
				var etemp1 = new EntryCard();
				etemp1.Title = "Prueba titulo";
				temp2.JourneyEntries.Add(etemp1);
				temp2.Entries = 1;
				JourneyDays.Add(temp2);

				var temp3 = new DayCard();
				temp3.Day = "4";
				temp3.Month = "2";
				JourneyDays.Add(temp3);
				//JourneyImages.Add(new ImageCard());

				daySelected = JourneyDays[0];
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

		#region Photos

		public ImageCard blanckImage = new ImageCard();
		public ImageCard BlanckImage
		{
			get => blanckImage;
			set
			{
				SetProperty(ref blanckImage, value);
			}
		}
		public int CardImagesHeight { get => CommonVariables.ImageCardMaxHeight; }

		#endregion

		#region Cover

		private ImageCard coverImage = new ImageCard()
		{
			ImageSour = CommonVariables.GetImage()
		};

		public ImageCard CoverImage
		{
			get => coverImage;
			set
			{
				SetProperty(ref coverImage, value);
			}
		}

		public int CoverImageHeight { get => CommonVariables.ImageMaxHeight; }

		#endregion

		#region DaySelected

		private DayCard daySelected = new DayCard();

		public DayCard DaySelected
		{
			get => daySelected;
			set
			{
				SetProperty(ref daySelected, value);
				foreach(DayCard day in JourneyDays)
				{
					day.Background = (Color)Application.Current.Resources["PrimaryFaded"];
				}
				JourneyDays.First(x => x == daySelected).Background = (Color)Application.Current.Resources["Primary"];
				//Color change = (Color) Application.Current.Resources["Primary"];
				//Color temp1 = JourneyDays.First(x => x == daySelected).Background;
				//SetProperty(ref temp1, change);
			}
		}

		public int DaySelectedNum { get; set; }

		#endregion

		#region Commands

		async internal void ModifyJourneyC()
		{
			await Shell.Current.GoToAsync($"{nameof(JourneySettingsView)}?{nameof(JourneySettingsViewModel.JourneyId)}={JourneyId}");
		}

		async internal void AddImageC()
		{
			ImageCard success = await CameraUtil.Photo(this);
			if (success != null)
			{
				JourneyImages.Add(success);
			}
		}

		async internal void AddToJourneyC()
		{
			AddToJourneyPopUp popup = new AddToJourneyPopUp(this);
			//popup.model.journey = JourneyCard;
		}

		//TO-DO checkear
		async internal void CreateEventC()
		{ // TO-DO Aqui mas alante podría pasarle el ID del Day y buscarlo en BBDD....
			await Shell.Current.GoToAsync($"{nameof(CreateEventView)}?{nameof(CreateEventViewModel.DaySelected)}={JourneyDays.IndexOf(DaySelected)}&{nameof(CreateEventViewModel.JourneyId)}={JourneyId}");
		}

		async internal void CreateEntryC()
		{
			await Shell.Current.GoToAsync($"{nameof(CreateEntryView)}?{nameof(CreateEntryViewModel.DaySelectedNum)}={DaySelectedNum}" +
																	$"&{nameof(CreateEntryViewModel.JourneyId)}={JourneyId}");
		}

		#endregion

		#region OnAction

		async void OnImageSelected(ImageCard image)
		{
			if (image == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			//await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
		}

		async void OnDaySelected(DayCard day)
		{
			if (day == null)
				return;

			DaySelected = day;
			// This will push the ItemDetailPage onto the navigation stack
			//await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
		}

		#endregion

		void JourneyDaysChanged(object sender, NotifyCollectionChangedEventArgs e)
		{

			if (e.NewItems != null)
				foreach (DayCard day in e.NewItems)
					day.PropertyChanged += OnPropertyChanged;

			if (e.OldItems != null)
				foreach (DayCard day in e.OldItems)
					day.PropertyChanged -= OnPropertyChanged;
		}

	}
}
