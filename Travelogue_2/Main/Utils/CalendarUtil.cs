using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Diagnostics;
using System.Linq;
using Travelogue_2.Main.BBDD;
using Travelogue_2.Main.Models;

namespace Travelogue_2.Main.Utils
{
	public static class CalendarUtil
	{

		public static async void CheckPermissions()
		{
			PermissionStatus statusCalendar = await CrossPermissions.Current.CheckPermissionStatusAsync<CalendarPermission>();
			if (statusCalendar != PermissionStatus.Granted)
			{
				statusCalendar = await CrossPermissions.Current.RequestPermissionAsync<CalendarPermission>();
			}

			Debug.WriteLine("Permision calendar : " + statusCalendar);

		}

		public async static void AddJourney(Journey journey)
		{
			CheckPermissions();

			var calendars = await CrossCalendars.Current.GetCalendarsAsync();
			var calendarEvent = new CalendarEvent
			{
				Name = journey.Name,
				Start = journey.IniDate,
				End = journey.EndDate,
				//Reminders = new List<CalendarEventReminder> { new CalendarEventReminder() }
			};

			// TO-DO : Usuario escoge calendario?....

			try
			{
				Calendar calendar = calendars.FirstOrDefault(x => x.AccountName == "Phone");
				await CrossCalendars.Current.AddOrUpdateEventAsync(calendar, calendarEvent);
			}
			catch(Exception e)
			{
				Debug.WriteLine("Exception : " + e.ToString());
			}
		}

	}
}
