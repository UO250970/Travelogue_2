using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Plugin.Permissions;
using System.Threading.Tasks;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;
using System.Diagnostics;
using System.Linq;
using Plugin.Settings.Abstractions;
using Travelogue_2.Main.Utils;
using System;
using Travelogue_2.Main.BBDD;

namespace Travelogue_2.Automatization
{
	static class Automatization
	{
		public static string Image1 = "/storage/emulated/0/Android/data/com.companyname.travelogue_2/files/Pictures/temp/IMG_20210421_115029.jpg";

		public static async Task CheckPermissionsAsync()
		{
			try
			{
				PermissionStatus statusLocation = CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>().Result;
				if (statusLocation != PermissionStatus.Granted)
				{
					statusLocation = CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>().Result;
				}

				Debug.WriteLine("Permision location : " + statusLocation);

				PermissionStatus statusCamera = CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>().Result;
				if (statusCamera != PermissionStatus.Granted)
				{
					statusCamera = CrossPermissions.Current.RequestPermissionAsync<CameraPermission>().Result;
				}

				Debug.WriteLine("Permision camera : " + statusCamera);

				PermissionStatus statusCalendar = CrossPermissions.Current.CheckPermissionStatusAsync<CalendarPermission>().Result;
				if (statusCalendar != PermissionStatus.Granted)
				{
					statusCalendar = CrossPermissions.Current.RequestPermissionAsync<CalendarPermission>().Result;
				}

				Debug.WriteLine("Permision calendar : " + statusCalendar);
			}
			catch(Exception e)
			{
				Debug.WriteLine("Error : " + e.StackTrace);
			}
		}

		public static void PrepareCountries()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceNameDestiny = "Travelogue_2.Resources.JSON.ISO2CountryChecked.txt";
			var resourceNameEmbassy = "Travelogue_2.Resources.JSON.ESEmbassyList.txt";
			string result = string.Empty;

			using (Stream stream = assembly.GetManifestResourceStream(resourceNameDestiny))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}
			var iso2ListDestiny = JsonConvert.DeserializeObject<List<Destiny>>(result);

			result = string.Empty;

			using (Stream stream = assembly.GetManifestResourceStream(resourceNameEmbassy))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}
			var iso2ListEmbassy = JsonConvert.DeserializeObject<List<Embassy>>(result);

			foreach (Destiny destiny in iso2ListDestiny)
			{
				destiny.Original = true;

				List<Embassy> listEmbassy = iso2ListEmbassy.Where(x => x.Country == destiny.Name)?.ToList();

				destiny.Embassies = listEmbassy;
			}

			DataBaseUtil.InsertDestinies(iso2ListDestiny);
		}

		public static void PrepareStyles()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceNameStyles = "Travelogue_2.Resources.JSON.Styles.txt";
			string result = string.Empty;

			using (Stream stream = assembly.GetManifestResourceStream(resourceNameStyles))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}
			var styles = JsonConvert.DeserializeObject<List<Style>>(result);

			DataBaseUtil.InsertStyles(styles);
		}

		private static void CreateFutur()
		{
			JourneyModel journey = DataBaseUtil.CreateJourney("StandardTrip Futur", DateTime.Today.AddDays(3), DateTime.Today.Date.AddDays(5));

			DataBaseUtil.JourneyInsertDestiny(journey, "Australia");
			DataBaseUtil.JourneyInsertDestiny(journey, "Barbados");
			DataBaseUtil.JourneyInsertDestiny(journey, "Georgia");

			EntryModel entry = DataBaseUtil.JourneyInsertEntry(journey, 1, "Standard");

			DataBaseUtil.EntryInsertText(entry, "Entry text");
			DataBaseUtil.JourneyInsertEvent(journey, 1, "Concierto Manin", TimeSpan.Parse("12:12"), "Calle lapus");
			DataBaseUtil.JourneyInsertReserv(journey, 1, 3, "Hotel", "Calle milagros", "985 3816 36");
		}

		private static void CreateOnCourse()
		{
			JourneyModel journey = DataBaseUtil.CreateJourney("StandardTrip Course", DateTime.Today.AddDays(-1), DateTime.Today.Date.AddDays(1));

			EntryModel entry = DataBaseUtil.JourneyInsertEntry(journey, 1, "Standard");

			DataBaseUtil.EntryInsertText(entry, "Entry text");
			DataBaseUtil.JourneyInsertEvent(journey, 1, "Concierto Manin", TimeSpan.Parse("12:12"), "Calle lapus");
			DataBaseUtil.JourneyInsertReserv(journey, 1, 3, "Reserva hotel", "Calle dandy", "985 3816 36");
		}

		private static void CreateFinished()
		{
			JourneyModel journey = DataBaseUtil.CreateJourney("StandardTrip Finished", DateTime.Today.AddDays(-5), DateTime.Today.Date.AddDays(-3));
			DataBaseUtil.JourneyInsertEvent(journey, 2, "Concierto Manin", TimeSpan.Parse("12:12"), "Calle lapus");

			DataBaseUtil.JourneyInsertEvent(journey, 1, "Concierto Pepita", TimeSpan.Parse("12:12"), "Calle romanov");
		}

		private static void ClearDB()
		{
			DataBaseUtil.ClearDataBase();
		}

		public static void PrepareBd(ISettings properties)
		{
            ClearDB();

			//_ = CheckPermissionsAsync();

			if (!DataBaseUtil.HasJourneis())
			{
				properties.Clear();
			}

			if (!DataBaseUtil.HasDestinies())
			{
				PrepareCountries();
			}
			DataBaseUtil.GetDestinies().ForEach(x => CommonVariables.AvailableDestinies.Add(x));

			if (!DataBaseUtil.HasStyles())
            {
				PrepareStyles();
            }
			DataBaseUtil.GetStyles().ForEach(x => CommonVariables.AvailableStyles.Add(x));

			CreateOnCourse();
			CreateFutur();
			CreateFinished();
		}

	}
}
