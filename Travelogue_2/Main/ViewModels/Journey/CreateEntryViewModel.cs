using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Models.Entries;
using Xamarin.Forms;


namespace Travelogue_2.Main.ViewModels.Journal
{
	public class CreateEntryViewModel : DataBaseViewModel
	{
		public string JourneyId { get; set; }
		public string DaySelected { get; set; }
		public ObservableCollection<DayCard> JourneyDays { get; }

		public Command CancelCommand { get; }
		public Command CreateCommand { get; }

		public CreateEntryViewModel()
		{
			CancelCommand = new Command(() => CancelC());
			CreateCommand = new Command(() => CreateC());

			JourneyDays = new ObservableCollection<DayCard>();

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
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
		}

		private int daySelectedNum;
		public int DaySelectedNum
		{
			get => daySelectedNum;
			set
			{
				SetProperty(ref daySelectedNum, value);
				var temp = JourneyDays?[daySelectedNum];
				Date = new DateTime(int.Parse(temp.Year), int.Parse(temp.Month), int.Parse(temp.Day));
			}
		}

		private DateTime date;

		public DateTime Date
		{
			get => date;
			set => SetProperty(ref date, value);
		}

		private DateTime minimumDate;
		public DateTime MinimumDate
		{
			get => minimumDate;
			set => SetProperty(ref minimumDate, value);
		}

		private DateTime maximumDate;
		public DateTime MaximumDate
		{
			get => maximumDate;
			set => SetProperty(ref maximumDate, value);
		}

		async internal void CancelC()
		{
			await Shell.Current.GoToAsync("..");
		}

		async internal void CreateC()
		{
			try
			{

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}
	}
}
