using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Travelogue_2.Main.BBDD;
using Travelogue_2.Main.Models;
using Xamarin.Forms;

namespace Travelogue_2.Main.Utils
{
    public static class CalendarUtil
    {

        public static async Task<PermissionStatus> CheckPermissions()
        {
            PermissionStatus statusCalendar = PermissionStatus.Unknown;
            try
            {
                statusCalendar = CrossPermissions.Current.CheckPermissionStatusAsync<CalendarPermission>().Result;
                if (statusCalendar != PermissionStatus.Granted)
                {
                    statusCalendar = await CrossPermissions.Current.RequestPermissionAsync<CalendarPermission>();
                }

                Debug.WriteLine("Permision calendar: " + statusCalendar);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error permision calendar: " + e.StackTrace);
            }

            return statusCalendar;
        }

        public async static void AddJourney(Journey journey)
        {
            await CheckPermissions();
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
            catch (Exception e)
            {
                Debug.WriteLine("Exception : " + e.ToString());
            }
        }

        internal static CalendarEventCollection GetJourneys(List<JourneyModel> journeys)
        {
            CalendarEventCollection collection = new CalendarEventCollection();

            foreach (JourneyModel jour in journeys)
            {
                CalendarInlineEvent evento = new CalendarInlineEvent();

                evento.AutomationId = jour.Id.ToString();
                evento.StartTime = jour.IniDate;
                evento.EndTime = jour.EndDate;
                evento.Subject = jour.Name;
                evento.Color = Color.FromHex("#3D6D9B");

                collection.Add(evento);
            }

            return collection;
        }
    }
}
