using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library.Create
{
	public class CreateJourneyViewModel : PhotoRendererModel
	{
		public Command AddCoverCommand { get; }
		public Command AddDestinyCommand { get; }
		public Command MoreInfoCommand { get; }
		public Command CancelCommand { get; }
		public Command SaveCommand { get; }

		public ObservableCollection<DayCard> DaysSelected { get; }
		public ObservableCollection<DestinyCard> DestiniesSelected { get; }
		public ObservableCollection<string> DestiniesList { get; }
		public Command<DestinyCard> DestinyTappedDelete { get; }

		public Command<DayCard> DayTapped { get; }
		

		public CreateJourneyViewModel()
		{
			AddCoverCommand = new Command(() => AddCoverC());

			AddDestinyCommand = new Command(() => AddDestinyC());
			MoreInfoCommand = new Command<string>((x) => MoreInfoC(x));

			CancelCommand = new Command(() => CancelC());
			SaveCommand = new Command(() => SaveC());

			DaysSelected = new ObservableCollection<DayCard>();
			DestiniesSelected = new ObservableCollection<DestinyCard>();
			DestiniesList = new ObservableCollection<string>();

			DayTapped = new Command<DayCard>(OnDayTapped);
			DestinyTappedDelete = new Command<DestinyCard>(OnDestinySelectedDelete);

			ExecuteLoadDataCommand();
		}

		#region CoverImage
		public EntryImageCard coverImage = new EntryImageCard();
		public EntryImageCard CoverImage
		{
			get => coverImage;
			set
			{
				SetProperty(ref coverImage, value);
			}
		}
		#endregion

		#region Title
		private string title = string.Empty;
		public string Title
		{
			get => title;
			set => SetProperty(ref title, value);
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
		private DateTime iniDate = DateTime.Today;
		public DateTime IniDate
		{
			get => iniDate;
			set => SetProperty(ref iniDate, value);
		}
		#endregion

		#region EndDate
		private DateTime endDate = DateTime.Today;
		public DateTime EndDate
		{
			get => endDate;
			set => SetProperty(ref endDate, value);
		}
		#endregion

		#region CorrectDestinyText
		private bool correctDestinyText;
		public bool CorrectDestinyText
		{
			get => correctDestinyText;
			set => SetProperty(ref correctDestinyText, value);
		}
		#endregion

		#region DestinyText
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
		#endregion

		#region ImageVisible
		private bool imageVisible = false;
		public bool ImageVisible
		{
			get => imageVisible;
			set => SetProperty(ref imageVisible, value);
		}
		#endregion

		#region LabelVisible
		private bool labelVisible = true;
		public bool LabelVisible
		{
			get => labelVisible;
			set => SetProperty(ref labelVisible, value);
		}
		#endregion

		public override void LoadData()
		{
			DestiniesList.Clear();
			IEnumerable<string> temp1 = CommonVariables.AvailableDestinies?.Select(x => x.Name);
			foreach (string destiny in temp1)
			{
				DestiniesList.Add(destiny);
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

		async internal void AddCoverC()
		{
			EntryImageCard success = await CameraUtil.Photo(this);
			if (success != null)
			{
				CoverImage = success;
			}
		}

		async internal void AddDestinyC()
		{
			if ( DestiniesSelected.Count <= CommonVariables.DestiniesInJourney )
			{
				Destiny destiny = CommonVariables.AvailableDestinies.Find(x => x.Name == DestinyText);
				DestinyCard temp = new DestinyCard();
				temp.Destiny = destiny.Name;
				temp.Code = destiny.Code;
				temp.Currency = destiny.Currency;
				if (!DestiniesSelected.Contains(temp))
				{
					DestiniesSelected.Add(temp);
				}
				else
				{
					await Alerter.AlertDestinyAlreadySelected();
				}
			}
			else
			{
				await Alerter.AlertTooManyDestiniesInJourney();
			}
			DestinyText = string.Empty;
		}

		async internal void MoreInfoC(string path)
		{
			await Browser.OpenAsync(path);
		}

		async internal void CancelC()
			=> await Shell.Current.GoToAsync("..");

		async internal void SaveC()
		{ 
			if (title.Equals(string.Empty))
			{
				await Alerter.AlertNoNameInJourney();
			} else
			{
				JourneyCard journey = new JourneyCard();
				journey.Name = Title;
				journey.IniDate = IniDate;
				journey.EndDate = EndDate;
				CalendarUtil.AddJourney(journey);
				//TO-DO create and store journey
				await Alerter.AlertJourneyCreated();
				//TO-DO checkear cuando empieza  eso y cambiar redirección
				await Shell.Current.GoToAsync("..");
			}
		}

		internal void CheckNewIniDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			if (iniDatePicker.Date.CompareTo(EndDate.Date) > 0) endDatePicker.Date = iniDatePicker.Date;
			//ExecuteLoadDataCommand();
		}

		internal void CheckNewEndDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			if (iniDatePicker.Date.CompareTo(EndDate.Date) > 0) iniDatePicker.Date = endDatePicker.Date;
			//ExecuteLoadDataCommand();
		}

		void OnDayTapped(DayCard day)
		{
			if (day == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			//await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
		}

		async void OnDestinySelectedDelete(DestinyCard destiny)
		{
			if (destiny == null)
				return;

			DestiniesSelected.Remove(destiny);

		}
	}
}
