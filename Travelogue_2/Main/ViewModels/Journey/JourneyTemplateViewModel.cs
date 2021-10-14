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
		public Command<ImageModel> ImageTapped { get; }
		public Command<DayModel> DayTapped { get; }
		public ObservableCollection<ImageModel> JourneyImages { get; }

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
			AddToEntryCommand = new Command<EntryModel>((x) => AddToEntryC(x));
			AddImageCommand = new Command(() => AddImageC());
			ModifyJourneyCommand = new Command(() => ModifyJourneyC());

			ViewImageCommand = new Command<ImageModel>((ImageCard) => ViewImageC(ImageCard));
			PhoneNumberTappedCommand = new Command<string>((x) => PhoneNumberTappedC(x));

			EditOrDeleteEventCommand = new Command<EventModel>((EventModel e) => EditOrDeleteEventC(e));
			EditOrDeleteEntryCommand = new Command<EntryModel>((EntryModel e) => EditOrDeleteEntryC(e));

			JourneyImages = new ObservableCollection<ImageModel>();
			JourneyDays = new ObservableCollection<DayModel>();

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

			DaySelected = JourneyDays[0];
		}

		public override void OnAppearing()
		{
			base.OnAppearing();

			CoverImage = DataBaseUtil.GetCoverFromJourney( int.Parse(journeyId)) ;

			if (DaySelected.Day != null)
            {
				var temp = DataBaseUtil.GetDaysFromJourneyId(int.Parse(journeyId));
				JourneyDays.Clear();

				temp.First(x => x.Date.Equals(DaySelected.Date))?.Select();
				temp.OrderBy(x => x.Date).ToList().ForEach(x => JourneyDays.Add(x));

				DaySelected = JourneyDays[DaySelectedNum];
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

		#region CoverImage
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
			ImageModel success = await CameraUtil.Photo(this, journeyId);
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

		async internal void AddToEntryC(EntryModel entryC)
		{
			await Shell.Current.GoToAsync($"{nameof(AddToEntryPopUp)}?&{nameof(AddToJourneyPopUpModel.JourneyId)}={JourneyId}" +
																	$"{nameof(AddToJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}&" +
																	$"{nameof(AddToJourneyPopUpModel.EntryId)}={entryC.Id}");
		}
		#endregion

		#region OnAction

		void OnDaySelected(DayModel day)
		{
			if (day == null)
				return;

			DaySelected = day;
			// This will push the ItemDetailPage onto the navigation stack
			//await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
		}

		#endregion

	}
}
