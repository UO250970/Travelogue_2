using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.BBDD;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Xamarin.Forms;
using Entry = Travelogue_2.Main.BBDD.Entry;
using Image = Travelogue_2.Main.BBDD.Image;

namespace Travelogue_2.Main.Utils
{
    public static class DataBaseUtil
    {

        public static void Start()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            DataBase.OpenDataBase(path);

            Automatization.Automatization.PrepareBd(CrossSettings.Current);
        }

        public static void ClearDataBase() => DataBase.ClearDataBase();

        public static string HasJourneis() => DataBase.HasJourneis();

        public static string HasDestinies() => DataBase.HasDestinies();

        public static void InsertDestinies(List<Destiny> list) => DataBase.InsertDestinies(list);

        public static void GetJourneyForJournal(int JourneyId)
		{
            /*Dictionary<DateTime, >
            Journey jour = DataBase.GetJourneyById(JourneyId);
             jour.Days()*/
		}

        public static JourneyModel GetJourneyById(string id)
        {
            Journey temp = DataBase.GetJourneyById( int.Parse(id) );

            return JourneyToModel(temp);
        }

        public static JourneyModel GetJourneyOnGoing()
		{
            Journey temp = DataBase.GetJourneys(State.OPEN).FirstOrDefault();

            return JourneyToModel(temp);
		}

        public static List<JourneyModel> GetJourneysCreated()
        {
            List<JourneyModel> temp = new List<JourneyModel>();
            List<Journey> tempDB = DataBase.GetJourneys(State.CREATED);
            foreach (Journey jour in tempDB)
			{
                temp.Add(JourneyToModel(jour));
			}
            return temp;
        }

        public static List<JourneyModel> GetJourneysClosed()
        {
            List<JourneyModel> temp = new List<JourneyModel>();
            List<Journey> tempDB = DataBase.GetJourneys(State.CLOSED);
            foreach (Journey jour in tempDB)
            {
                temp.Add(JourneyToModel(jour));
            }
            return temp;
        }

        public static JourneyModel CreateJourney(string name, DateTime ini, DateTime end)
		{
            Journey jour = new Journey(name, ini, end);

            if (jour.StartedAlready() && jour.FinishedAlready())
			{
                jour.JourneyState = State.CLOSED;
			} else if (jour.StartedAlready() && !jour.FinishedAlready())
			{
                jour.JourneyState = State.OPEN;
			} else
			{
                jour.JourneyState = State.CREATED;
			}

            DataBase.InsertJourney(jour);

            // TODO - SOLO EN PRUEBAS, que no quiero mil viajes creados en mi movil...
            //CalendarUtil.AddJourney(jour);

            return JourneyToModel(jour);
		}

        public static void JourneyInsertDestiny(JourneyModel journey, string name)
		{
            Journey jour = JourneyFromModel(journey);
            Destiny destiny = DataBase.GetDestinyByName(name);

            jour.Destinies.Add(destiny);
            destiny.Journeys.Add(jour);

            DataBase.UpdateJourney(jour);
            DataBase.UpdateDestiny(destiny);
        }

        public static EntryModel JourneyInsertEntry(JourneyModel journey, int dayInt, string title)
        {
            Day day = JourneyFromModel(journey).Days[dayInt - 1];
            Entry entry = new Entry();
            entry.Title = title;
            entry.Time = DateTime.Now.ToString("HH:mm");

            day.Entries.Add(entry);

            DataBase.InsertEntry(entry);
            DataBase.UpdateDay(day);

            return EntryToModel(entry);
        }

        internal static void JourneyInsertEvent(JourneyModel journey, int dayInt, string title, string address)
        {
            Day day = JourneyFromModel(journey).Days[dayInt - 1];

            Event evento = new Event();
            evento.Title = title;
            evento.Address = address;
            evento.Time = DateTime.Now.ToString("HH:mm");
            //evento.Days.Add(day);

            DataBase.InsertEvent(evento);

            day.Events.Add(evento);

            DataBase.UpdateDay(day);
        }
        
        internal static void JourneyInsertReserv(JourneyModel journey, int dayInt, int duration, string title, string address, string phoneNumber)
        {
            List<Day> days = JourneyFromModel(journey).Days;

            Event evento = new Event();
            evento.Title = title;
            evento.Address = address;
            evento.PhoneNumber = phoneNumber;
            evento.Reserv = true;
            evento.Time = DateTime.Now.ToString("HH:mm");

            DataBase.InsertEvent(evento);

            days[dayInt - 1].Events.Add(evento);

            for (int i = 0; i < duration - 1; i++)
            {
                days[dayInt + i].Events.Add(evento);
            }

            DataBase.UpdateDays(days);
            //DataBase.UpdateEvent(evento);
        }

        public static void EntryInsertImage(EntryModel entry, string path, string name, string caption)
		{
            Entry ent = EntryFromModel(entry);

            Image image = new Image();
            image.Date = ent.Day.Date;
            image.Path = path;
            image.Name = name;
            image.Caption = caption;

            EntryData entryData = new EntryData();
            entryData.Time = DateTime.Now.ToString("HH:mm");
            entryData.Image = image;

            DataBase.InsertEntryData(entryData);

            ent.Content.Add(entryData);

            DataBase.UpdateEntry(ent);
        }

        public static void EntryInsertText(EntryModel entry, string text)
		{
            Entry ent = EntryFromModel(entry);

            EntryData entryData = new EntryData();
            entryData.Time = DateTime.Now.ToString("HH:mm");
            entryData.Text = text;

            DataBase.InsertEntryData(entryData);

            ent.Content.Add(entryData);

            DataBase.UpdateEntry(ent);
        }
        
        public static DestinyModel GetDestinyByName(string name) 
        {
            Destiny destiny = DataBase.GetDestinyByName(name);
			return DestinyToModel(destiny);
        }

        public static List<DayModel> GetDaysFromJourney(JourneyModel journey)
        {
            Journey temp = JourneyFromModel(journey);
            return DaysToModel(temp.Days);
        }

        public static List<DayModel> GetDaysFromJourneyId(string journeyId)
        {
            Journey temp = DataBase.GetJourneyById(int.Parse(journeyId));
            return DaysToModel(temp.Days);
        }

        public static EventModel GetEventById(string id)
        {
            Event temp = DataBase.GetEventById(int.Parse(id));

            return EventToModel(temp);
        }

        public static bool SaveEvent(EventModel evento)
        {
            Event temp = EventFromModel(evento);

            if ( !evento.Title.Equals(temp.Title) ) temp.Title = evento.Title;
            if ( !evento.Time.Equals(temp.Time) ) temp.Time = evento.Time;
            if ( !evento.Address.Equals(temp.Address) ) temp.Address = evento.Address;
            if ( !evento.PhoneNumber.Equals(temp.PhoneNumber) ) temp.PhoneNumber = evento.PhoneNumber;

            bool Days = evento.IniDay.Equals(temp.Days.First().Date) && evento.EndDay.Equals(temp.Days.Last().Date);

            if ( !Days )
            {
                List<Day> days = DataBase.GetDaysBetweenDays(evento.IniDay, evento.EndDay);

                List<Day> daysFilled = days.FindAll(x => x.Events.Count >= CommonVariables.EventsInDay);
                if (daysFilled.Count != 0)
                {
                    Alerter.AlertTooManyEventsInDay();
                    return false;
                }
                else
                {
                    temp.Days.Clear();
                    temp.Days = days;
                }
            }

            if (DataBase.UpdateEvent(temp)) Alerter.AlertEventSaved();
            return true;
        }

        public static EntryModel GetEntryById(string id)
        {
            Entry temp = DataBase.GetEntryById(int.Parse(id));

            return EntryToModel(temp);
        }

        #region Models

        private static JourneyModel JourneyToModel(Journey journey)
		{
            JourneyModel temp = new JourneyModel();

            if (journey is null)
			{
                return temp;
			}

            temp.Id = journey.Id;
            temp.Name = journey.Name;
            if (journey.Cover is null)
            {
                temp.Image = CommonVariables.GetImage();
            } else
            {
                //Stream stream = file.GetStream();
                //temp.Image = ImageSource.FromStream(() => stream);
                temp.Image = ImageSource.FromFile(journey.Cover.Path);
            }
            temp.IniDate = journey.IniDate;
            temp.EndDate = journey.EndDate;

            return temp;
        }
        private static Journey JourneyFromModel(JourneyModel journey) => DataBase.GetJourneyById(journey.Id);

        private static DestinyModel DestinyToModel(Destiny destiny)
        {
            DestinyModel temp = new DestinyModel();
            temp.Code = destiny.Code;
            temp.Currency = destiny.Currency;
            temp.Destiny = destiny.Name;
            foreach ( Embassy emb in destiny.Embassies)
			{
                temp.EmbassiesCities.Add(emb.City);
                temp.Embassies.Add(emb.City, emb.PhoneNumber);
			}

            return temp;
        }
        private static Destiny DestinyFromModel(DestinyModel destiny) => DataBase.GetDestinyByName(destiny.Destiny);

        private static List<DayModel> DaysToModel(List<Day> days)
        {
            List<DayModel> temp = new List<DayModel>();

            foreach (Day day in days)
            {
                DayModel tempDay = new DayModel();

                tempDay.Id = day.Id;
                tempDay.Day = day.Date.Day.ToString();
                tempDay.Month = day.Date.Month.ToString();
                tempDay.Year = day.Date.Year.ToString();

                tempDay.JourneyEvents = new ObservableCollection<EventModel>( EventsToModel(day.Events) );
                tempDay.JourneyEntries = new ObservableCollection<EntryModel>( EntriesToModel(day.Entries) );

                temp.Add(tempDay);
            }

            return temp;
        }

        private static EventModel EventToModel(Event evento)
        {
            EventModel temp = new EventModel
            {
                Id = evento.Id,
                Time = evento.Time,
                Title = evento.Title,
                Address = evento.Address,
                PhoneNumber = evento.PhoneNumber != null ? evento.PhoneNumber :  string.Empty,
                Reservation = evento.Reserv,
                IniDay = evento.Days.First().Date,
                EndDay = evento.Days.Last().Date
        };

            return temp;
        }
        private static List<EventModel> EventsToModel(List<Event> events)
        {
            List<EventModel> temp = new List<EventModel>();

            foreach (Event evento in events)
            {
                temp.Add( EventToModel(evento) );
            }

            return temp;
        }
        private static Event EventFromModel(EventModel evento) => DataBase.GetEventById(evento.Id);

        private static EntryModel EntryToModel(Entry entry)
        {              
            EntryModel tempEntry = new EntryModel();

            tempEntry.Id = entry.Id;
            tempEntry.Title = entry.Title;
            tempEntry.Time = entry.Time;

            foreach (EntryData data in entry.Content)
			{
                if (data.Text != "")
				{
                    EntryTextModel temp1 = new EntryTextModel();
                    temp1.Id = data.Id;
                    temp1.Time = data.Time;
                    temp1.Text = data.Text;

                    tempEntry.Content.Add(temp1);
				}
				else
				{
                    EntryImageModel temp2 = new EntryImageModel();
                    temp2.Id = data.Id;
                    temp2.ImageName = data.Image.Name;
                    temp2.ImagePath = data.Image.Path;
                    temp2.ImageCaption = data.Image.Caption;
                    temp2.JourneyName = data.Entry.Day.Journey.Name;

                    tempEntry.Content.Add(temp2);
				}

			}

            return tempEntry;
        }
        private static List<EntryModel> EntriesToModel(List<Entry> entries)
        {
            List<EntryModel> temp = new List<EntryModel>();

            foreach (Entry entry in entries)
            {
                temp.Add( EntryToModel(entry) );
            }

            return temp;
        }
        private static Entry EntryFromModel(EntryModel entry) => DataBase.GetEntryById(entry.Id);

        #endregion
    }
}
