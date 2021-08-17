using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
	[QueryProperty(nameof(JourneyId), nameof(JourneyId))]
	[QueryProperty(nameof(DaySelectedNum), nameof(DaySelectedNum))]
	[QueryProperty(nameof(EntryTextId), nameof(EntryTextId))]
	[QueryProperty(nameof(EntryImageId), nameof(EntryImageId))]
	public class AddToJourneyPopUpModel : PhotoRendererModel
	{
		public string journeyId;
		public string JourneyId
		{
			get => journeyId;
			set => journeyId = value;
		}


		public string entryTextId;
		public string EntryTextId
		{
			get => entryTextId;
			set => entryTextId = value;
		}

		public string entryImageId;
		public string EntryImageId
		{
			get => entryImageId;
			set => entryImageId = value;
		}

		public string daySelectedNum;
		public string DaySelectedNum
		{
			get => daySelectedNum;
			set
			{
				daySelectedNum = value;
				LoadData();
			}
		}

		public Command CreateEventCommand { get; }

		public Command CreateReservationCommand { get; }

		public Command CreateEntryCommand { get; }

		public Command AddImageCommand { get; }
		public Command CreateTextCommand { get; }
		public Command CreateImageCommand { get; }
		public Command CancelCommand { get; }

		public AddToJourneyPopUpModel()
		{
			CreateEventCommand = new Command(() => CreateEventC());

			CreateReservationCommand = new Command(() => CreateReservationC());

			CreateEntryCommand = new Command(() => CreateEntryC());

			AddImageCommand = new Command(() => AddImageC());
			CreateTextCommand = new Command(() => CreateTextC());
			CreateImageCommand = new Command(() => CreateImageC());

			CancelCommand = new Command(() => CancelC());
		}

		public override void LoadData()
		{
			if (JourneyId != null && DaySelectedNum != null)
			{
				// TODO - Pilla el journey de BBDD y pilla el list de dias o whatever
				Days = DataBaseUtil.GetDaysFromJourneyId( Integer.ParseInt(JourneyId) )
					.OrderBy(x => x.Date)
					.ToList();
				
				DaySelected = Days[int.Parse(DaySelectedNum)].Date;
				MaxDaySelected = Days.Last().Date;
				MinDaySelected = Days.First().Date;
			}
		}

		#region Title

		public string Title { get; set; } = string.Empty;

		#endregion

		#region Days
		List<DayModel> Days { get; set; }
		#endregion

		#region DaySelected

		private DateTime daySelected = DateTime.Today;
		public DateTime DaySelected
		{
			get => daySelected;
			set
			{
				SetProperty(ref daySelected, value);
			}
		}

		#endregion

		#region IniDaySelected

		private DateTime iniDaySelected = DateTime.Today;
		public DateTime IniDaySelected
		{
			get => iniDaySelected;
			set
			{
				SetProperty(ref iniDaySelected, value);
			}
		}

		#endregion

		#region EndDaySelected

		private DateTime endDaySelected = DateTime.Today;
		public DateTime EndDaySelected
		{
			get => endDaySelected;
			set
			{
				SetProperty(ref endDaySelected, value);
			}
		}

		#endregion

		#region MinDaySelected

		private DateTime minDaySelected = DateTime.Today;
		public DateTime MinDaySelected
		{
			get => minDaySelected;
			set
			{
				SetProperty(ref minDaySelected, value);
			}
		}

		#endregion

		#region MaxDaySelected

		private DateTime maxDaySelected = DateTime.Today;
		public DateTime MaxDaySelected
		{
			get => maxDaySelected;
			set
			{
				SetProperty(ref maxDaySelected, value);
			}
		}

		#endregion

		#region Time
		private TimeSpan time = new TimeSpan();
		public TimeSpan Time
		{
			get => time;
			set => SetProperty(ref time, value);
		}
		#endregion

		#region Location

		private string location = string.Empty;
		public string Location
		{
			get => location;
			set
			{
				SetProperty(ref location, value);
			}
		}

		#endregion

		#region PhoneNumber
		private string phoneNumber = string.Empty;
		public string PhoneNumber
		{
			get => phoneNumber;
			set
			{
				SetProperty(ref phoneNumber, value);
			}
		}
		#endregion

		#region Text
		private string text = string.Empty;
		public string Text
		{
			get => text;
			set
			{
				SetProperty(ref text, value);
			}
		}
		#endregion

		#region Image
		private EntryImageModel image = new EntryImageModel();
		public EntryImageModel Image
		{
			get => image;
			set
			{
				SetProperty(ref image, value);
			}
		}
		#endregion

		#region Caption
		private string caption = string.Empty;
		public string Caption
		{
			get => caption;
			set
			{
				SetProperty(ref caption, value);
			}
		}
		#endregion

		#region EventCommands
		async internal void CreateEventC()
		{
			if (DaySelected == null)
			{
				await Alerter.AlertNoDaySelected();
			}
			else if (Title == string.Empty)
			{
				await Alerter.AlertNoTitleInEvent();
			}
			else
			{
				int dayInt = Days.FindIndex(x => x.Date.Equals(DaySelected.Date)) + 1;
				DataBaseUtil.JourneyInsertEvent( Integer.ParseInt(JourneyId), dayInt, Title, Time.ToString(), Location);
				Back();
			}
		}

		async internal void CreateReservationC()
		{
			if (IniDaySelected == null && EndDaySelected == null)
			{
				await Alerter.AlertNoDaySelected();
			}
			else if (Title == string.Empty)
			{
				await Alerter.AlertNoTitleInReservation();
			}
			else
			{
				int duration = (int)(EndDaySelected - IniDaySelected).TotalDays + 1;
				int dayInt = Days.FindIndex(x => x.Date.Equals(IniDaySelected.Date)) + 1;
				DataBaseUtil.JourneyInsertReserv( Integer.ParseInt(JourneyId), dayInt, duration, Title, Location, PhoneNumber);
				Back();
			}
		}
		#endregion

		#region EntryCommands
		async internal void CreateEntryC()
		{
			if (DaySelected == null)
			{
				await Alerter.AlertNoDaySelected();
			} 
			else if (Title == string.Empty)
			{
				await Alerter.AlertNoTitleInEntry();
			} else
			{
				int dayInt = Days.FindIndex(x => x.Date.Equals(IniDaySelected.Date)) + 1;
				DataBaseUtil.JourneyInsertEntry(Integer.ParseInt(JourneyId), dayInt, Title);
				Back();
			}
		}
		#endregion

		async internal void AddImageC()
		{
			EntryImageModel success = await CameraUtil.Photo(this);
			if (success != null)
			{
				Image = success;
			}
		}

		async internal void CreateTextC()
		{
			if (Text == string.Empty)
			{
				await Alerter.AlertNoText();
			}
			else
			{

				await Alerter.AlertTextAdded();
				Back();
			}
		}

		async internal void CreateImageC()
		{
			if (Image.ImageSour == null)
			{
				await Alerter.AlertNoImageSelected();
			}
			else
			{
				await Alerter.AlertImageAdded();
				Back();
			}
		}

		async internal void CancelC()
		{
			if (Title != string.Empty || Location != string.Empty || PhoneNumber != string.Empty 
				|| Text != string.Empty || Caption != string.Empty)
			{
				bool result = await Alerter.AlertInfoWillBeLost();

				if (result) Back();
			}
			else
			{
				Back();
			}
		}
	}
}
