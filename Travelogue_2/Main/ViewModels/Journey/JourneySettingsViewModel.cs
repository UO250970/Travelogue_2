using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Travelogue_2.Main.Utils;
using System;
using System.Collections.Generic;

namespace Travelogue_2.Main.ViewModels.Journey
{
	[QueryProperty(nameof(JourneyId), nameof(JourneyId))]
	public class JourneySettingsViewModel : PhotoRendererModel
	{
		public string journeyId;
		public string JourneyId
		{
			get
			{
				return journeyId;
			}
			set
			{
				journeyId = value;
				LoadData();
			}
		}

		public Command ModifyCoverCommand { get; }
		public Command MoreInfoCommand { get; }
		public Command PhoneNumberTappedCommand { get; }
		public Command DeleteJourneyCommand { get; }
		public ObservableCollection<DestinyModel> JourneyDestinies { get; set; }

		public JourneySettingsViewModel()
		{
			coverImage = new EntryImageModel();
			JourneyDestinies = new ObservableCollection<DestinyModel>();

			ModifyCoverCommand = new Command(x => ModifyCoverC());
			MoreInfoCommand = new Command<string>((x) => MoreInfoC(x));
			PhoneNumberTappedCommand = new Command<string>((x) => PhoneNumberTappedC(x));
			DeleteJourneyCommand = new Command(x => DeleteJourneyC());
		}

		public override void LoadData()
		{

			if (JourneyId != null)
			{
				Journey = DataBaseUtil.GetJourneyById(int.Parse(JourneyId));
				JourneyName = Journey.Name;
				State = Journey.JourneyState;

				if (Journey.CoverId >= 1)
				{
					ImageModel cover = DataBaseUtil.GetImageById(journey.CoverId);
					CoverImage = cover;
				}

				IniDate = Journey.IniDate;
				IniDateEnabled = (State.Equals(State.CLOSED) || State.Equals(State.OPEN)) ? false : true;
				EndDate = Journey.EndDate;
				EndDateEnabled = State.Equals(State.CLOSED) ? false : true;

				JourneyDestinies = new ObservableCollection<DestinyModel>(DataBaseUtil.GetDestiniesFromJourney(journey));
			}
		}

		private JourneyModel journey;

		public JourneyModel Journey
		{
			get => journey;
			set => SetProperty(ref journey, value);
		}

		#region State
		private State State { get; set; }
		#endregion

		#region JourneyName
		public string journeyName;

		public string JourneyName
		{
			get => journeyName;
			set
			{
				SetProperty(ref journeyName, value);
				Journey.Name = JourneyName;
			}
		}
		#endregion

		#region CoverImage
		private ImageModel coverImage = new ImageModel();
		public ImageModel CoverImage
		{
			get => coverImage;
			set
			{
				SetProperty(ref coverImage, value);
				DataBaseUtil.SaveImage(coverImage);
			}
		}
		#endregion

		#region MinimumDate
		private DateTime minimumDate = DateTime.Today;
		public DateTime MinimumDate
		{
			get => minimumDate;
			set => SetProperty(ref minimumDate, value);
		}
		#endregion

		#region IniDate
		private bool iniDateEnabled = true;
		public bool IniDateEnabled
        {
			get => iniDateEnabled;
			set => SetProperty(ref iniDateEnabled, value);
		}

		private DateTime iniDate = DateTime.Today;
		public DateTime IniDate
		{
			get => iniDate;
			set
			{
				if (value.CompareTo(minimumDate) < 0) MinimumDate = iniDate;
				SetProperty(ref iniDate, value);
			}
		}
		#endregion

		#region EndDate
		private bool endDateEnabled = true;
		public bool EndDateEnabled
		{
			get => endDateEnabled;
			set => SetProperty(ref endDateEnabled, value);
		}

		private DateTime endDate = DateTime.Today;
		public DateTime EndDate
		{
			get => endDate;
			set => SetProperty(ref endDate, value);
		}
		#endregion

		async internal void ModifyCoverC()
		{
			ImageModel success = await CameraUtil.Photo(this);
			if (success != null)
			{
				CoverImage = success;
			}
		}

		async internal void MoreInfoC(string path)
		{
			await Browser.OpenAsync(path);
		}

		INotifications notificationManager = DependencyService.Get<INotifications>();

		internal void PhoneNumberTappedC(string number)
		{
			PhoneDialer.Open(number);

			notificationManager.SendNotification("¡Estamos de viaje!", "Desliza si quieres añadir una entrada...");
		}

		internal void DeleteJourneyC()
        {
			DataBaseUtil.DeleteJourney( int.Parse(journeyId) );
        }

		internal void CheckNewIniDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			if (iniDatePicker.Date.CompareTo(endDatePicker.Date) > 0)
				endDatePicker.Date = iniDatePicker.Date;
		}

		internal void CheckNewEndDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			if (endDatePicker.Date.CompareTo(iniDatePicker.Date) < 0)
				iniDatePicker.Date = endDatePicker.Date;
		}

		internal override async void Back()
		{
			Journey.IniDate = IniDate;
			Journey.EndDate = EndDate;
			if( await DataBaseUtil.SaveJourney(Journey) && DataBaseUtil.SaveJourneyDestinies(Journey, new List<DestinyModel>(JourneyDestinies)) ) base.Back();
		}

	}
}
