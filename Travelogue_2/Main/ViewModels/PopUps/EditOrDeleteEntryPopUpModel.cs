using System;
using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
	[QueryProperty(nameof(JourneyId), nameof(JourneyId))]
	[QueryProperty(nameof(DaySelectedNum), nameof(DaySelectedNum))]
	[QueryProperty(nameof(EntryId), nameof(EntryId))]
	public class EditOrDeleteEntryPopUpModel : DataBaseViewModel
	{
		public string journeyId;
		public string JourneyId
		{
			get => journeyId;
			set => journeyId = value;
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

		public Command SaveCommand { get; }
		public Command DeleteCommand { get; }
		public Command CancelCommand { get; }

		public EditOrDeleteEntryPopUpModel()
		{
			SaveCommand = new Command(() => SaveyC());
			DeleteCommand = new Command(() => DeleteC());

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

		async internal void SaveyC()
		{

		}

		async internal void DeleteC()
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
