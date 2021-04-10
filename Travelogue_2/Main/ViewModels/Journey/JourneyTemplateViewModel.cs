using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Models.Entries;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.ViewModels.PopUps;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.PopUps;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journal
{
	[QueryProperty(nameof(JourneyId), nameof(JourneyId))]
	public class JourneyTemplateViewModel : PhotoRendererModel
	{
		public string JourneyId { get; set; }
		public Command AddImageCommand { get; }
		public Command<EntryImageCard> ImageTapped { get; }
		public Command<DayCard> DayTapped { get; }
		public ObservableCollection<EntryImageCard> JourneyImages { get; }
		public ObservableCollection<DayCard> JourneyDays { get; }
		public ObservableCollection<EventCard> JourneyEvents { get; }
		public ObservableCollection<EntryCard> JourneyEntries { get; }

		/** Commands*/
		public Command AddEventCommand { get; }
		public Command AddEntryCommand { get; }
		public Command AddToEntryCommand { get; }
		public Command ModifyJourneyCommand { get; }
		public Command<EventCard> EditOrDeleteEventCommand { get; }
		public Command<EntryCard> EditOrDeleteEntryCommand { get; }
		/** */

		public JourneyTemplateViewModel()
		{
			AddEventCommand = new Command(() => AddEventC());
			AddEntryCommand = new Command(() => AddEntryC());
			AddToEntryCommand = new Command(() => AddToEntryC());
			AddImageCommand = new Command(() => AddImageC());
			ModifyJourneyCommand = new Command(() => ModifyJourneyC());

			EditOrDeleteEventCommand = new Command<EventCard>((EventCard e) => EditOrDeleteEventC(e));
			EditOrDeleteEntryCommand = new Command<EntryCard>((EntryCard e) => EditOrDeleteEntryC(e));

			JourneyImages = new ObservableCollection<EntryImageCard>();
			JourneyDays = new ObservableCollection<DayCard>();
			JourneyDays.CollectionChanged += JourneyDaysChanged;
			JourneyEvents = new ObservableCollection<EventCard>();
			JourneyEntries = new ObservableCollection<EntryCard>();

			ImageTapped = new Command<EntryImageCard>(OnImageSelected);
			DayTapped = new Command<DayCard>(OnDaySelected);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			JourneyId = "1";
			var temp = new DayCard();
			temp.Day = "2";
			temp.Month = "2";
			temp.Year = "2021";
			JourneyDays.Add(temp);

			var temp2 = new DayCard();
			temp2.Day = "3";
			temp2.Month = "2";
			temp2.Year = "2021";
			var etemp1 = new EntryCard();
			etemp1.Title = "Prueba titulo";
			var ectemp1 = new EntryTextCard();
			ectemp1.Text = "Hoy me divertí mucho corriendo detrás de patos :')";
			ectemp1.Time = DateTime.Now.ToString("HH:mm");  // TODO - Pasar a documetnación https://stackoverflow.com/questions/11107465/getting-only-hour-minute-of-datetime/11107508
			etemp1.Content.Add(ectemp1);
			var ectemp2 = new EntryImageCard();
			ectemp2.Time = DateTime.Now.ToString("HH:mm");
			etemp1.Content.Add(ectemp2);
			temp2.JourneyEntries.Add(etemp1);
			temp2.Entries = 1;
			var etemp2 = new EventCard();
			etemp2.Text = "Concierto de Lady Gaga";
			etemp2.Address = "Calle fulanito";
			etemp2.Time = "12:00";
			temp2.JourneyEvents.Add(etemp2);
			temp2.Events = 1;
			JourneyDays.Add(temp2);

			var temp3 = new DayCard();
			temp3.Day = "4";
			temp3.Month = "2";
			temp3.Year = "2021";
			JourneyDays.Add(temp3);
			//JourneyImages.Add(new ImageCard());

			DaySelected = JourneyDays[0];
		}

		public override void OnAppearing()
		{
			IsBusy = true;

			var temp = new ObservableCollection<DayCard>(JourneyDays);
			JourneyDays.Clear();

			foreach (DayCard day in temp)
			{
				JourneyDays.Add(day);
			}
		}

		#region Photos

		public EntryImageCard blanckImage = new EntryImageCard();
		public EntryImageCard BlanckImage
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

		private EntryImageCard coverImage = new EntryImageCard()
		{
			ImageSour = CommonVariables.GetImage()
		};

		public EntryImageCard CoverImage
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
					day.Background = (Color) App.Current.Resources["PrimaryFaded"];
				}
				var selected = JourneyDays.First(x => x == daySelected);
				selected.Background = (Color) Application.Current.Resources["Primary"];

				var temp = new ObservableCollection<DayCard>(JourneyDays);
				JourneyDays.Clear();

				foreach (DayCard day in temp)
				{
					JourneyDays.Add(day);
				}

				DaySelectedNum = JourneyDays.IndexOf(selected);
			}
		}

		public int DaySelectedNum = 0;
		#endregion

		#region Commands

		async internal void ModifyJourneyC()
		{
			await Shell.Current.GoToAsync($"{nameof(JourneySettingsView)}?{nameof(JourneySettingsViewModel.JourneyId)}={JourneyId}");
		}

		async internal void AddImageC()
		{
			EntryImageCard success = await CameraUtil.Photo(this);
			if (success != null)
			{
				JourneyImages.Add(success);
			}
		}

		async internal void AddEventC()
		{
			//TO-DO checkear
			await Shell.Current.GoToAsync($"{nameof(AddEventPopUp)}?{nameof(AddToJourneyPopUpModel.JourneyId)}={JourneyId}&" +
																	$"{nameof(AddToJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}");
		}

		async internal void AddEntryC()
		{
			//TO-DO checkear
			await Shell.Current.GoToAsync($"{nameof(AddEntryPopUp)}?{nameof(AddToJourneyPopUpModel.JourneyId)}={JourneyId}&" +
																	$"{nameof(AddToJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}");
		}

		async internal void AddToEntryC()
		{
			await Shell.Current.GoToAsync($"{nameof(AddToEntryPopUp)}?{nameof(AddToJourneyPopUpModel.JourneyId)}={JourneyId}&" +
																	$"{nameof(AddToJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}");
		}

		async internal void EditOrDeleteEventC(EventCard eventC)
		{
			await Alerter.AlertEmptyData();
		}
		
		async internal void EditOrDeleteEntryC(EntryCard entryC)
		{
			await Alerter.AlertEmptyData();
		}
		#endregion

		#region OnAction

		async void OnImageSelected(EntryImageCard image)
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
