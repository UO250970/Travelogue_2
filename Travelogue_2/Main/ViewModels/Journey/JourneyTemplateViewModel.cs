using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.ViewModels.PopUps;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.PopUps;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journey
{
	public class JourneyTemplateViewModel : PhotoRendererModel
	{
		protected string journeyId;

		public string JourneyId { get => journeyId; }

		public Command AddImageCommand { get; }
		public Command<EntryImageModel> ImageTapped { get; }
		public Command<DayModel> DayTapped { get; }
		public ObservableCollection<EntryImageModel> JourneyImages { get; }

		private ObservableCollection<DayModel> journeyDays;

		public ObservableCollection<DayModel> JourneyDays
		{
			get => journeyDays;
			set => SetProperty(ref journeyDays, value);
		}

		/** Commands*/
		public Command AddEventCommand { get; }
		public Command AddEntryCommand { get; }
		public Command AddToEntryCommand { get; }
		public Command ModifyJourneyCommand { get; }
		public Command PhoneNumberTappedCommand { get; }
		public Command<EventModel> EditOrDeleteEventCommand { get; }
		public Command<EntryModel> EditOrDeleteEntryCommand { get; }
		/** */

		public JourneyTemplateViewModel()
        {
			AddEventCommand = new Command(() => AddEventC());
			AddEntryCommand = new Command(() => AddEntryC());
			AddToEntryCommand = new Command(() => AddToEntryC());
			AddImageCommand = new Command(() => AddImageC());
			ModifyJourneyCommand = new Command(() => ModifyJourneyC());

			ViewImageCommand = new Command<ImageModel>((ImageCard) => ViewImageC(ImageCard));
			PhoneNumberTappedCommand = new Command<string>((x) => PhoneNumberTappedC(x));

			EditOrDeleteEventCommand = new Command<EventModel>((EventModel e) => EditOrDeleteEventC(e));
			EditOrDeleteEntryCommand = new Command<EntryModel>((EntryModel e) => EditOrDeleteEntryC(e));

			JourneyImages = new ObservableCollection<EntryImageModel>();
			JourneyDays = new ObservableCollection<DayModel>();
			//JourneyDays.CollectionChanged += JourneyDaysChanged;
			//JourneyEvents = new ObservableCollection<EventModel>();
			//JourneyEntries = new ObservableCollection<EntryModel>();

			ImageTapped = new Command<EntryImageModel>(OnImageSelected);
			DayTapped = new Command<DayModel>(OnDaySelected);

			//ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			if (JourneyId != null)
			{
				JourneyModel journey = DataBaseUtil.GetJourneyById( int.Parse(JourneyId) );
				JourneyName = journey.Name;

				if (journey.CoverId >= 1)
                {
					ImageModel cover = DataBaseUtil.GetImageById(journey.CoverId);
					CoverImage = cover;
				}

				// TODO - Chekear acciones según estado

				JourneyDays = new ObservableCollection<DayModel>( DataBaseUtil.GetDaysFromJourney(journey) );
			}

			//JourneyId = "1";
			/**var temp = new DayModel();
			temp.Day = "2";
			temp.Month = "2";
			temp.Year = "2021";
			JourneyDays.Add(temp);

			var temp2 = new DayModel();
			temp2.Day = "3";
			temp2.Month = "2";
			temp2.Year = "2021";
			var etemp1 = new EntryModel();
			etemp1.Title = "Prueba titulo";
			var ectemp1 = new EntryTextModel(4);
			ectemp1.Text = "Hoy me divertí mucho corriendo detrás de patos :')";
			ectemp1.Time = DateTime.Now.ToString("HH:mm");  // TODO - Pasar a documetnación https://stackoverflow.com/questions/11107465/getting-only-hour-minute-of-datetime/11107508
			etemp1.Content.Add(ectemp1);
			var ectemp2 = new EntryImageModel();
			ectemp2.Time = DateTime.Now.ToString("HH:mm");
			etemp1.Content.Add(ectemp2);
			temp2.JourneyEntries.Add(etemp1);
			//temp2.Entries = 1;
			var etemp2 = new EventModel();
			etemp2.Text = "Concierto de Lady Gaga";
			etemp2.Address = "Calle fulanito";
			etemp2.Time = "12:00";
			temp2.JourneyEvents.Add(etemp2);
			//temp2.Events = 1;
			JourneyDays.Add(temp2);

			var temp3 = new DayModel();
			temp3.Day = "4";
			temp3.Month = "2";
			temp3.Year = "2021";
			JourneyDays.Add(temp3);
			//JourneyImages.Add(new ImageCard());*/

			DaySelected = JourneyDays[0];
		}

		public override void OnAppearing()
		{
			base.OnAppearing();

			if (DaySelected.Day != null)
            {
				var temp = DataBaseUtil.GetDaysFromJourneyId(int.Parse(journeyId));
				JourneyDays.Clear();
				temp.First(x => x.Date.Equals(DaySelected.Date))?.Select();

				JourneyDays = new ObservableCollection<DayModel>(temp);

				DaySelected = JourneyDays[DaySelectedNum];

				/*foreach (DayModel day in temp)
				{
					if (day.Date.Equals(DaySelected.Date)) day.Select();
					JourneyDays.Add(day);
				}*/
			}

		}

		#region Name

		private string journeyName;

		public string JourneyName
        {
			get => journeyName;
			set => SetProperty(ref journeyName, value);
        }

		#endregion

		#region Cover
		private ImageModel coverImage = new ImageModel();
		public ImageModel CoverImage
		{
			get => coverImage;
			set => SetProperty(ref coverImage, value);
		}
		#endregion

		#region DaySelected
		private DayModel daySelected = new DayModel();

		public DayModel DaySelected
		{
			get => daySelected;
			set
			{
				SetProperty(ref daySelected, value);

				foreach(DayModel day in JourneyDays)
				{
					day.Unselect();
				}
				var selected = JourneyDays.First(x => x.Date == daySelected.Date);
				selected.Select();

				var temp = new ObservableCollection<DayModel>(JourneyDays);
				JourneyDays.Clear();

				foreach (DayModel day in temp)
				{
					JourneyDays.Add(day);
				}

				DaySelectedNum = JourneyDays.IndexOf(selected);
			}
		}

		public int DaySelectedNum = 0;
		#endregion

		#region Commands

		INotifications notificationManager = DependencyService.Get<INotifications>();

		internal void PhoneNumberTappedC(string number)
		{
			PhoneDialer.Open(number);

			notificationManager.SendNotification("¡Estamos de viaje!", "Desliza si quieres añadir una entrada...");
		}

		async internal void ModifyJourneyC()
		{
			await Shell.Current.GoToAsync($"{nameof(JourneySettingsView)}?{nameof(JourneySettingsViewModel.JourneyId)}={JourneyId}");
		}

		async internal void AddImageC()
		{
			EntryImageModel success = await CameraUtil.Photo(this);
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

		async internal void EditOrDeleteEventC(EventModel eventC)
		{
			await Shell.Current.GoToAsync($"{nameof(EditOrDeleteEventPopUp)}?{nameof(EditOrDeleteFromJourneyPopUpModel.JourneyId)}={JourneyId}&" +
																	$"{nameof(EditOrDeleteFromJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}&" +
																	$"{nameof(EditOrDeleteFromJourneyPopUpModel.EventId)}={eventC.Id}");
		}

		async internal void AddEntryC()
		{
			await Shell.Current.GoToAsync($"{nameof(AddEntryPopUp)}?{nameof(AddToJourneyPopUpModel.JourneyId)}={JourneyId}&" +
																	$"{nameof(AddToJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}");
		}

		async internal void EditOrDeleteEntryC(EntryModel entryC)
		{
			await Shell.Current.GoToAsync($"{nameof(EditOrDeleteEntryPopUp)}?{nameof(EditOrDeleteFromJourneyPopUpModel.JourneyId)}={JourneyId}&" +
																	$"{nameof(EditOrDeleteFromJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}&" +
																	$"{nameof(EditOrDeleteFromJourneyPopUpModel.EntryId)}={entryC.Id}");
		}

		async internal void AddToEntryC()
		{
			await Shell.Current.GoToAsync($"{nameof(AddToEntryPopUp)}?{nameof(AddToJourneyPopUpModel.JourneyId)}={JourneyId}&" +
																	$"{nameof(AddToJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}");
		}
		#endregion

		#region OnAction

		async void OnImageSelected(EntryImageModel image)
		{
			if (image == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			//await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
		}

		async void OnDaySelected(DayModel day)
		{
			if (day == null)
				return;

			DaySelected = day;
			// This will push the ItemDetailPage onto the navigation stack
			//await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
		}

		#endregion

		/*void JourneyDaysChanged(object sender, NotifyCollectionChangedEventArgs e)
		{

			if (e.NewItems != null)
				foreach (DayCard day in e.NewItems)
					day.PropertyChanged += OnPropertyChanged;

			if (e.OldItems != null)
				foreach (DayCard day in e.OldItems)
					day.PropertyChanged -= OnPropertyChanged;
		}*/
	}
}
