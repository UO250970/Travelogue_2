﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Utils;
using System;

namespace Travelogue_2.Main.ViewModels.Journal
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
		public ObservableCollection<DestinyCard> JourneyDestinies { get; }

		public JourneySettingsViewModel()
		{
			coverImage = new EntryImageCard();
			JourneyDestinies = new ObservableCollection<DestinyCard>();

			ModifyCoverCommand = new Command(x => ModifyCoverC());
			MoreInfoCommand = new Command<string>((x) => MoreInfoC(x));
			PhoneNumberTappedCommand = new Command<string>((x) => PhoneNumberTappedC(x));

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			journeyId = "1";
			JourneyName = "Prueba titulo";

			JourneyDestinies.Clear();

			DestinyCard temp = new DestinyCard();
			Destiny tempDestiny = CommonVariables.AvailableDestinies.Find(x => x.Name == "Canada");
			temp.Destiny = tempDestiny.Name;
			temp.Code = tempDestiny.Code;
			temp.Currency = tempDestiny.Currency;
			Dictionary<string, string> tempEmbassies = new Dictionary<string, string>();
			foreach (Embassy embassy in tempDestiny.Embassies)
			{
				tempEmbassies.Add(embassy.City, embassy.PhoneNumber);
			}
			temp.Embassies = tempEmbassies;

			JourneyDestinies.Add(temp);

			coverImage.ImageSour = CommonVariables.GetImage(); 
		}

		#region JourneyName
		public string journeyName;

		public string JourneyName
		{
			get => journeyName;
			set => SetProperty(ref journeyName, value);
		}
		#endregion

		#region CoverImage
		public ImageCard coverImage;
		public ImageCard CoverImage
		{
			get => coverImage;
			set
			{
				SetProperty(ref coverImage, value);
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

		async internal void ModifyCoverC()
		{
			ImageCard success = await CameraUtil.Photo(this);
			if (success != null)
			{
				CoverImage = success;
			}
		}

		async internal void MoreInfoC(string path)
		{
			await Browser.OpenAsync(path);
		}

		ILocalNotifications notificationManager = DependencyService.Get<ILocalNotifications>();

		internal void PhoneNumberTappedC(string number)
		{
			PhoneDialer.Open(number);

			notificationManager.SendNotification("Title", "Message");
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

	}
}