using System;
using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
	[QueryProperty(nameof(JourneyId), nameof(JourneyId))]
	[QueryProperty(nameof(DaySelectedNum), nameof(DaySelectedNum))]
	[QueryProperty(nameof(EventId), nameof(EventId))]
	[QueryProperty(nameof(EntryId), nameof(EntryId))]
	public class EditOrDeleteFromJourneyPopUpModel : DataBaseViewModel
	{
		public string journeyId;
		public string JourneyId
		{
			get => journeyId;
			set => journeyId = value;
		}

		public string eventId;
		public string EventId
		{
			get => eventId;
			set => eventId = value;
		}

		public string entryId;
		public string EntryId
		{
			get => entryId;
			set => entryId = value;
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

		public Command SaveEventCommand { get; }
		public Command DeleteEventCommand { get; }

		public Command SaveEntryCommand { get; }
		public Command DeleteEntryCommand { get; }

		public Command SaveTextCommand { get; }
		public Command DeleteTextCommand { get; }

		public Command CancelCommand { get; }

		public EditOrDeleteFromJourneyPopUpModel()
		{
			SaveEventCommand = new Command(() => SaveEventC());
			DeleteEventCommand = new Command(() => DeleteEventC());

			SaveEntryCommand = new Command(() => SaveEntryC());
			DeleteEntryCommand = new Command(() => DeleteEntryC());

			SaveTextCommand = new Command(() => SaveyTextC());
			DeleteTextCommand = new Command(() => DeleteTextC());

			CancelCommand = new Command(() => CancelC());
		}

		public override void LoadData()
		{
			if (JourneyId != null && DaySelectedNum != null)
			{
			}

			ObservableCollection<DateTime> Days = new ObservableCollection<DateTime>
			{
				new DateTime(2021, 02, 02),
				new DateTime(2021, 02, 03),
				new DateTime(2021, 02, 04)
			};

			DaySelected = Days[int.Parse(DaySelectedNum)];
			MinDaySelected = Days.First();
			MaxDaySelected = Days.Last();
		}

		#region Title

		public string Title { get; set; } = string.Empty;

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

		async internal void SaveEventC()
		{

		}

		async internal void DeleteEventC()
		{

		}

		async internal void SaveEntryC()
		{

		}

		async internal void DeleteEntryC()
		{

		}

		async internal void SaveyTextC()
		{

		}

		async internal void DeleteTextC()
		{

		}

		async internal void CancelC()
		{
			if (Title != string.Empty)
			{
				bool result = await Alerter.AlertInfoWillBeLost();

				if (result) await Shell.Current.GoToAsync("..");
			}
			else
			{
				await Shell.Current.GoToAsync("..");
			}
		}

	}
}
