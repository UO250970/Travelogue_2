using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.ViewModels.PopUps;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.PopUps;
using Travelogue_2.Main.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journey
{
	public class JourneyTemplateViewModel : PhotoRendererModel
	{
		public Command AddImageCommand { get; }
		public Command<EntryImageModel> ImageTapped { get; }
		public Command<DayModel> DayTapped { get; }
		public ObservableCollection<ImageModel> JourneyImages;

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
		public Command<IEntry> EditOrDeleteEntryDataCommand { get; }
		/** */

		public JourneyTemplateViewModel()
        {
			AddEventCommand = new Command(() => AddEventC());
			AddEntryCommand = new Command(() => AddEntryC());
			AddToEntryCommand = new Command<EntryModel>((x) => AddToEntryC(x));
			AddImageCommand = new Command(() => AddImageC());
			ModifyJourneyCommand = new Command(() => ModifyJourneyC());

			ViewImageCommand = new Command<EntryImageModel>((x) => ViewImageC(x));
			PhoneNumberTappedCommand = new Command<string>((x) => PhoneNumberTappedC(x));

			EditOrDeleteEventCommand = new Command<EventModel>((EventModel e) => EditOrDeleteEventC(e));
			EditOrDeleteEntryCommand = new Command<EntryModel>((EntryModel e) => EditOrDeleteEntryC(e));
			EditOrDeleteEntryDataCommand = new Command<IEntry>((IEntry e) => EditOrDeleteEntryDataC(e));

			JourneyImages = new ObservableCollection<ImageModel>();
			JourneyDays = new ObservableCollection<DayModel>();

			DayTapped = new Command<DayModel>(OnDaySelected);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			if (CurrentJourneyId != "0")
            {
				JourneyModel journey = DataBaseUtil.GetJourneyById(int.Parse(CurrentJourneyId));
				JourneyName = journey.Name;

				if (journey.CoverId >= 1)
				{
					ImageModel cover = DataBaseUtil.GetImageById(journey.CoverId);
					CoverImage = cover;
				}

				// TODO - Chekear acciones según estado
				JourneyDays = new ObservableCollection<DayModel>(DataBaseUtil.GetDaysFromJourney(journey));

				DaySelected = JourneyDays[0];

				switch (journey.JourneyState)
                {
					case State.OPEN:
						Background = (Color) Application.Current.Resources["Primary"];
						break;
					case State.CREATED:
						StateCreated();
						break;
					case State.CLOSED:
						StateClosed();
						break;
				}

				JourneyImages = new ObservableCollection<ImageModel>(DataBaseUtil.GetImagesFromJourney(journey));
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

		#region State

		private string stateMessage = "";
		public string StateMessage
        {
			get => stateMessage;
			set => SetProperty(ref stateMessage, value);
		}

		private bool stateVisible = false;
		public bool StateVisible
        {
			get => stateVisible;
			set => SetProperty(ref stateVisible, value);
        }


		private Color background;
		public Color Background
        {
			get => background;
			set => SetProperty(ref background, value);
        }


		#endregion

		#region Settings
		private bool settingsVisible = true;

		public bool SettingsVisible
        {
			get => settingsVisible;
			set => SetProperty(ref settingsVisible, value);
        }
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
			await Shell.Current.GoToAsync($"{nameof(JourneySettingsView)}");
		}

		async internal void AddImageC()
		{
			ImageModel success = await CameraUtil.Photo(this);
			if (success != null)
			{
				JourneyImages.Add(success);
			}
		}

		async internal void AddEventC()
		{
			//TO-DO checkear
			await Shell.Current.GoToAsync($"{nameof(AddEventPopUp)}?{nameof(AddToJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}");
		}

		async internal void EditOrDeleteEventC(EventModel eventC)
		{
			await Shell.Current.GoToAsync($"{nameof(EditOrDeleteEventPopUp)}?{nameof(EditOrDeleteFromJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}&" +
																	$"{nameof(EditOrDeleteFromJourneyPopUpModel.EventId)}={eventC.Id}");
		}

		async internal void AddEntryC()
		{
			await Shell.Current.GoToAsync($"{nameof(AddEntryPopUp)}?{nameof(AddToJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}");
		}

		async internal void EditOrDeleteEntryC(EntryModel entryC)
		{
			await Shell.Current.GoToAsync($"{nameof(EditOrDeleteEntryPopUp)}?{nameof(EditOrDeleteFromJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}&" +
																	$"{nameof(EditOrDeleteFromJourneyPopUpModel.EntryId)}={entryC.Id}");
		}

		async internal void EditOrDeleteEntryDataC(IEntry entryDataC)
        {
			await Shell.Current.GoToAsync($"{nameof(EditOrDeleteFromEntryPopUp)}?{nameof(EditOrDeleteFromJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}&" +
																	$"{nameof(EditOrDeleteFromJourneyPopUpModel.EntryDataId)}={entryDataC.Id}");
		}

		async internal void AddToEntryC(EntryModel entryC)
		{
			await Shell.Current.GoToAsync($"{nameof(AddToEntryPopUp)}?{nameof(AddToJourneyPopUpModel.DaySelectedNum)}={DaySelectedNum}&" +
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

		/**
		 * El usuario podrá:
		 *		- Modificar el nombre
		 *		- Modificar la cover
		 *		- Modificar las fechas
		 *		- Modificar los destinos
		 *		- Modificar los eventos
		 *		- Modificar las entradas
		 *		- Modificar la información
		 *		- Eliminar el viaje
		 */
		private void StateCreated()
        {
			Background = Color.FromHex(CommonVariables.CreatedColorHex);
			StateMessage = Resources["CreatedJourneyMessage"];
			StateVisible = true;
		}

		/** 
		 * El usuario podrá:
		 *		- Modificar los eventos
		 *		- Modificar las entradas
		 *		- Modificar la información
		 *		
		 * El usuario no podrá:
		 *		- Modificar el nombre
		 *		- Modificar la cover
		 *		- Modificar las fechas
		 *		- Modificar los destinos
		 *		- Eliminar el viaje
		 */
		private void StateClosed()
        {
			Background = Color.FromHex(CommonVariables.ClosedColorHex);
			StateMessage = Resources["ClosedMessage"];
			StateVisible = true;
			SettingsVisible = false;
		}

		/**
		 * El usuario podrá a un viaje abierto:
		 *		- Modificar el nombre
		 *		- Modificar la cover
		 *		- Modificar la fecha FINAL del viaje
		 *		- Modificar los destinos
		 *		- Modificar los eventos
		 *		- Modificar las entradas
		 *		- Modificar la información
		 *		- Eliminar el viaje
		 *		
		 *	El usuario no podrá:
		 *		- Modificar la fecha INICIAL del viaje
		 */

	}
}
