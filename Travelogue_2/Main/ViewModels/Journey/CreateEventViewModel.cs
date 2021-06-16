using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Travelogue_2.Main.Models;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journal
{
	[QueryProperty("JourneyId", "JourneyId")]
	[QueryProperty("DaySelected", "DaySelected")]
	public class CreateEventViewModel : DataBaseViewModel
	{
		public string JourneyId { get; set; }

		private int daySelected;
		public int DaySelected
		{
			get => daySelected;
			set
			{
				daySelected = value;
				var currentDate = JourneyDays?[DaySelected];
				Date = new DateTime(int.Parse(currentDate.Year), int.Parse(currentDate.MonthNum), int.Parse(currentDate.Day));
			}
		}

		public ObservableCollection<DayModel> JourneyDays { get; }

		public Command CancelCommand { get; }
		public Command CreateCommand { get; }

		public CreateEventViewModel()
		{
			CancelCommand = new Command(() => CancelC());
			CreateCommand = new Command(() => CreateC());

			JourneyDays = new ObservableCollection<DayModel>();

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			var temp = new DayModel();
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
			temp2.JourneyEntries.Add(etemp1);
			//temp2.Entries = 1;
			JourneyDays.Add(temp2);

			var temp3 = new DayModel();
			temp3.Day = "4";
			temp3.Month = "2";
			temp3.Year = "2021";
			JourneyDays.Add(temp3);

			var minimumDate = JourneyDays.First();
			MinimumDate = new DateTime(int.Parse(minimumDate.Year), int.Parse(minimumDate.MonthNum), int.Parse(minimumDate.Day));

			var maximumDate = JourneyDays.Last();
			MaximumDate = maximumDate.ToDateTime();
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

		private string title = string.Empty;

		public string Title
		{
			get => title;
			set => SetProperty(ref title, value);
		}

		private string description = string.Empty;

		public string Description
		{
			get => description;
			set => SetProperty(ref description, value);
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
