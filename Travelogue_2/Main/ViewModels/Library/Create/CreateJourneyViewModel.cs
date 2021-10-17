using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Models;
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

		public ObservableCollection<DayModel> DaysSelected { get; }
		public ObservableCollection<DestinyModel> DestiniesSelected { get; }
		public ObservableCollection<string> DestiniesList { get; }
		public Command<DestinyModel> DestinyTappedDelete { get; }

		//public Command<DayModel> DayTapped { get; }
		

		public CreateJourneyViewModel()
		{
			AddCoverCommand = new Command(() => AddCoverC());

			AddDestinyCommand = new Command(() => AddDestinyC());
			MoreInfoCommand = new Command<string>((x) => MoreInfoC(x));

			CancelCommand = new Command(() => CancelC());
			SaveCommand = new Command(() => SaveC());

			DestiniesSelected = new ObservableCollection<DestinyModel>();
			DestiniesList = new ObservableCollection<string>();

			DestinyTappedDelete = new Command<DestinyModel>(OnDestinySelectedDelete);

			CurrentJourneyId = DataBaseUtil.CreateEmptyJourney();

			ExecuteLoadDataCommand();
		}

		#region CoverImage
		public ImageModel coverImage = new ImageModel();
		public ImageModel CoverImage
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
		private DateTime iniDate;
		public DateTime IniDate
		{
			get => iniDate;
			set => SetProperty(ref iniDate, value);
		}
		#endregion

		#region EndDate
		private DateTime endDate;
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

			IniDate = DataBaseUtil.GetNextDayAvailable();
			EndDate = IniDate;
		}

		async internal void AddCoverC()
		{
			ImageModel success = await CameraUtil.Photo(this);
			if (success != null)
			{
				CoverImage = success;
			}
		}

		async internal void AddDestinyC()
		{
			if ( DestiniesSelected.Count <= CommonVariables.DestiniesInJourney )
			{
				DestinyModel destiny = DataBaseUtil.GetDestinyByName(DestinyText);
				
				if (!DestiniesSelected.Contains(destiny))
				{
					DestiniesSelected.Add(destiny);
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
				JourneyModel temp = DataBaseUtil.CreateJourney(Title, IniDate, EndDate, CoverImage);

				if (temp != null)
                {
					await Shell.Current.GoToAsync("..");
				}
				//TO-DO create and store journey
				//TO-DO checkear cuando empieza  eso y cambiar redirección
			}
		}

		internal void CheckNewIniDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			if (iniDatePicker.Date.CompareTo(EndDate.Date) > 0) endDatePicker.Date = iniDatePicker.Date;
		}

		internal void CheckNewEndDate(DatePicker iniDatePicker, DatePicker endDatePicker)
		{
			if (iniDatePicker.Date.CompareTo(EndDate.Date) > 0) iniDatePicker.Date = endDatePicker.Date;
		}

		void OnDestinySelectedDelete(DestinyModel destiny)
		{
			if (destiny == null)
				return;

			DestiniesSelected.Remove(destiny);
		}
	}
}
