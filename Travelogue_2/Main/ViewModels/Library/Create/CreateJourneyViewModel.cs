using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library.Create
{
	public class CreateJourneyViewModel : BaseViewModel
	{
		public Command AddDestinyCommand { get; }
		public Command CancelCommand { get; }
		public Command SaveCommand { get; }

		public ObservableCollection<DayCard> DaysSelected { get; }
		public ObservableCollection<DestinyCard> DestiniesSelected { get; }
		public ObservableCollection<string> DestiniesList { get; }

		public Command<DayCard> DayTapped { get; }
		

		public CreateJourneyViewModel()
		{
			AddDestinyCommand = new Command(() => AddDestinyC());

			CancelCommand = new Command(() => CancelC());
			SaveCommand = new Command(() => SaveC());

			DaysSelected = new ObservableCollection<DayCard>();
			DestiniesSelected = new ObservableCollection<DestinyCard>();
			DestiniesList = new ObservableCollection<string>();

			DayTapped = new Command<DayCard>(OnDayTapped);

			ExecuteLoadDataCommand();
		}


		private string title;
		public string Title
		{
			get => title;
			set => SetProperty(ref title, value);
		}

		private DateTime minimumDate = DateTime.Today;
		public DateTime MinimumDate
		{
			get => minimumDate;
			set => SetProperty(ref minimumDate, value);
		}

		private DateTime iniDate = DateTime.Today;
		public DateTime IniDate
		{
			get => iniDate;
			set => SetProperty(ref iniDate, value);
		}

		private DateTime endDate = DateTime.Today;
		public DateTime EndDate
		{
			get => endDate;
			set => SetProperty(ref endDate, value);
		}

		private bool correctDestinyText;
		public bool CorrectDestinyText
		{
			get => correctDestinyText;
			set => SetProperty(ref correctDestinyText, value);
		}

		private string destinyText;
		public string DestinyText
		{
			get => destinyText;
			set 
			{
				if (DestiniesList.Contains(value))
				{
					CorrectDestinyText = true;
				} else
				{
					CorrectDestinyText = false;
				}
				SetProperty(ref destinyText, value);
			}
		}

		async Task ExecuteLoadDataCommand()
		{
			IsBusy = true;

			try
			{
				DestiniesList.Clear();
				foreach(string data in CommonVariables.AvailableLanguages)
				{
					DestiniesList.Add(data);
				}

				DaysSelected.Clear();

				DateTime dateTemp = iniDate;
				do
				{
					DayCard temp = new DayCard();
					temp.Day = dateTemp.Day.ToString();
					temp.Month = dateTemp.Month.ToString();
					temp.Entries = 0;
					temp.Events = 0;
					DaysSelected.Add(temp);

					dateTemp = dateTemp.AddDays(1);
				}
				while (dateTemp.CompareTo(endDate) <= 0);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		async internal void AddDestinyC()
		{
			DestinyCard temp = new DestinyCard();
			temp.Destiny = destinyText;
			temp.Code = destinyText;
			temp.Currency = "Euros";
			DestiniesSelected.Add(temp);
			destinyText = "";
		}

		async internal void CancelC()
			=> await Shell.Current.GoToAsync("..");


		async internal void SaveC()
		{ }
		//=> await Shell.Current.GoToAsync(nameof(ClosedJourneysView));

		internal void CheckNewIniDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			ExecuteLoadDataCommand();
		}

		internal void CheckNewEndDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			ExecuteLoadDataCommand();
		}

		void OnDayTapped(DayCard day)
		{
			if (day == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			//await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
		}

	}
}
