using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

            DayTracker.Start();
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

        public static JourneyModel GetJourneyById(int JourneyId)
        {
            Journey temp = DataBase.GetJourneyById( JourneyId );

            return JourneyToModel(temp);
        }

        public static List<JourneyModel> GetJourneys()
		{
            List<JourneyModel> temp = new List<JourneyModel>();
            List<Journey> tempDB = DataBase.GetJourneis();
            foreach (Journey jour in tempDB)
            {
                temp.Add(JourneyToModel(jour));
            }
            return temp;
        }

        public static ObservableDictionary<string, List<ImageModel>> GetJourneisWithImages()
		{
            ObservableDictionary<string, List<ImageModel>> temp = new ObservableDictionary<string, List<ImageModel>>();

            List<ImageModel> Images = GetImages();
            List<string> viajes = GetJourneys().Select(x => x.Name).ToList();

            if (Images.Count != 0)
			{
                foreach (string s in viajes)
                {
                    var tempList = Images.Where(x => x.Journey.ToUpper().Equals(s.ToUpper())).ToList();
                    if (tempList.Count != 0)
                    {
                        temp.Add(s, tempList);
                    }
                }
            }

            return temp;
        }

        internal static string GetNameFromJourney(string journeyId)
        {
            return DataBase.GetNameFromJourney(journeyId);
        }

        public static JourneyModel GetJourneyOnGoing()
		{
            Journey temp = DataBase.GetJourneis(State.OPEN).FirstOrDefault();

            return JourneyToModel(temp);
		}

        public static List<JourneyModel> GetJourneysCreated()
        {
            List<JourneyModel> temp = new List<JourneyModel>();
            List<Journey> tempDB = DataBase.GetJourneis(State.CREATED);
            foreach (Journey jour in tempDB)
			{
                temp.Add(JourneyToModel(jour));
			}
            return temp;
        }

        public static List<JourneyModel> GetJourneysClosed()
        {
            List<JourneyModel> temp = new List<JourneyModel>();
            List<Journey> tempDB = DataBase.GetJourneis(State.CLOSED);
            foreach (Journey jour in tempDB)
            {
                temp.Add(JourneyToModel(jour));
            }
            return temp;
        }

        public static List<ImageModel> GetImages()
        {
            List<ImageModel> collection = new List<ImageModel>();
            List<Image> tempCollection = DataBase.GetImages();
            foreach (Image imag in tempCollection)
            {
                collection.Add(ImageToModel(imag));
            }

            return collection;
        }

        public static ImageModel CreateImage(string path, string caption, string journey = "")
		{
            Image image = new Image(path, caption, journey);

            DataBase.InsertImage(image);

            return ImageToModel(image);
		}

        public static JourneyModel CreateJourney(string name, DateTime ini, DateTime end, ImageModel cover = null)
		{
            if (DataBase.ExistsJourneyByName(name))
            {
                _ = Alerter.AlertJourneyNameInUse();
                return null;
            } 
            if (!DataBase.CheckDatesAreEmpty(ini, end))
			{
                _ = Alerter.AlertDatesAlreadyInUse();
                return null;
			}
			else
            {
				Journey jour = new Journey(name, ini, end)
				{
					JourneyState = State.CREATED,
				};

                if (cover != null)
                {
                    cover.Journey = name;
                    DataBase.UpdateImage(ImageFromModel(cover));

                    jour.Cover = ImageFromModel(cover);
                }

				DataBase.InsertJourney(jour);

                Journey journeyUpdated = DataBase.FindJourney(jour);

                // TODO - SOLO EN PRUEBAS, que no quiero mil viajes creados en mi movil...
                //CalendarUtil.AddJourney(journeyUpdated);
                DayTracker.UpdateDataBaseWithJourney(journeyUpdated);

                Alerter.AlertJourneyCreated();

                return JourneyToModel(journeyUpdated);
            }
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

        internal static void JourneyInsertEntry(int journeyId, int dayInt, string title)
        {
            JourneyModel temp = GetJourneyById(journeyId);
            JourneyInsertEntry(temp, dayInt, title);
        }

        public static void JourneyRemoveDestiny(JourneyModel journey, string name)
        {
            Journey jour = JourneyFromModel(journey);
            Destiny destiny = DataBase.GetDestinyByName(name);

            jour.Destinies.Remove(destiny);
            destiny.Journeys.Remove(jour);

            DataBase.UpdateJourney(jour);
            DataBase.UpdateDestiny(destiny);
        }

        public static EntryModel JourneyInsertEntry(JourneyModel journey, int dayInt, string title)
        {
            Day day = JourneyFromModel(journey).Days[dayInt - 1];
			Entry entry = new Entry
			{
				Title = title,
				Time = DateTime.Now.ToString("HH:mm")
			};

			day.Entries.Add(entry);

            DataBase.InsertEntry(entry);
            DataBase.UpdateDay(day);

            return EntryToModel(entry);
        }
        
        internal static void JourneyInsertEvent(int journeyId, int dayInt, string title, TimeSpan time, string address)
        {
            JourneyModel temp = GetJourneyById(journeyId);
            JourneyInsertEvent(temp, dayInt, title, time, address);
        }

        internal static void JourneyInsertEvent(JourneyModel journey, int dayInt, string title, TimeSpan time, string address)
        {
            Day day = JourneyFromModel(journey).Days[dayInt - 1];

			Event evento = new Event
			{
				Title = title,
				Address = address,
				Time = time.ToString()
			};

			DataBase.InsertEvent(evento);

            day.Events.Add(evento);

            DataBase.UpdateDay(day);
        }

        internal static void JourneyInsertReserv(int journeyId, int dayInt, int duration, string title, string address, string phoneNumber)
        {
            JourneyModel temp = GetJourneyById(journeyId);
            JourneyInsertReserv(temp, dayInt, duration, title, address, phoneNumber);
        }

        internal static void JourneyInsertReserv(JourneyModel journey, int dayInt, int duration, string title, string address, string phoneNumber)
        {
            List<Day> days = JourneyFromModel(journey).Days;

			Event evento = new Event
			{
				Title = title,
				Address = address,
				PhoneNumber = phoneNumber,
				Reserv = true,
				Time = DateTime.Now.ToString()
			};

			DataBase.InsertEvent(evento);

            days[dayInt - 1].Events.Add(evento);

            for (int i = 0; i < duration - 1; i++)
            {
                days[dayInt + i].Events.Add(evento);
            }

            DataBase.UpdateDays(days);
            //DataBase.UpdateEvent(evento);
        }

        public static void EntryInsertImage(EntryModel entry, string path, string caption)
		{
            Entry ent = EntryFromModel(entry);

			Image image = new Image
			{
				Date = ent.Day.Date,
				Path = path,
				Caption = caption,
				Journey = ent.Day.Journey.Name
			};

			EntryData entryData = new EntryData
			{
				Time = DateTime.Now.ToString("HH:mm"),
				Image = image
			};

			DataBase.InsertEntryData(entryData);

            ent.Content.Add(entryData);

            DataBase.UpdateEntry(ent);
        }

        public static void EntryInsertText(EntryModel entry, string text)
		{
            Entry ent = EntryFromModel(entry);

			EntryData entryData = new EntryData
			{
				Time = DateTime.Now.ToString("HH:mm"),
				Text = text
			};

			DataBase.InsertEntryData(entryData);

            ent.Content.Add(entryData);

            DataBase.UpdateEntry(ent);
        }
        
        public static DestinyModel GetDestinyByName(string name) 
        {
            Destiny destiny = DataBase.GetDestinyByName(name);
			return destiny == null ? null : DestinyToModel(destiny);
        }

        public static List<DestinyModel> GetDestiniesFromJourney(JourneyModel journey)
        {
            Journey temp = JourneyFromModel(journey);
            return DestiniesToModel(temp.Destinies);
        }

        public static List<DayModel> GetDaysFromJourney(JourneyModel journey)
        {
            Journey temp = JourneyFromModel(journey);
            return DaysToModel(temp.Days);
        }

        public static List<DayModel> GetDaysFromJourneyId(int JourneyId)
        {
            Journey temp = DataBase.GetJourneyById( JourneyId );
            return DaysToModel(temp.Days);
        }

        public static EventModel GetEventById(int EventId)
        {
            Event temp = DataBase.GetEventById( EventId );

            return temp == null ? null : EventToModel(temp);
        }

        public static EntryModel GetEntryById(int EntryId)
        {
            Entry temp = DataBase.GetEntryById( EntryId );

            return temp == null ? null : EntryToModel(temp);
        }

        public static ImageModel GetImageById(int ImageId)
        {
            Image temp = DataBase.GetImageById( ImageId );

            return temp == null ? null : ImageToModel(temp);
        }

        public static bool SaveJourneyDestinies(JourneyModel journey, List<DestinyModel> destinies)
		{
            List<DestinyModel> tempList = GetDestiniesFromJourney(journey);
            if (!GetDestiniesFromJourney(journey).SequenceEqual(destinies))
			{
                tempList.ForEach( x => JourneyRemoveDestiny(journey, x.Destiny) );
                destinies.ForEach( y => JourneyInsertDestiny(journey, y.Destiny) );
			}
            return true;
		}

        public static async Task<bool> SaveJourney(JourneyModel journey)
        {
            Journey temp = JourneyFromModel(journey);

            if (temp.CoverId != journey.CoverId)
            {
                Image coverTemp = ImageFromModel(GetImageById(journey.CoverId));
                temp.Cover = coverTemp;
            }
            if (temp.Name != journey.Name) temp.Name = journey.Name;

            DataBase.UpdateJourney(temp);

            if (temp.IniDate != journey.IniDate || temp.EndDate != journey.EndDate) return await UpdadteDates(journey);
            
            return true;
        }

        public static async Task<bool> UpdadteDates(JourneyModel journey)
        {
            if (DataBase.CheckDatesAreEmpty(journey.IniDate, journey.EndDate, journey.Id))
            {
                Journey temp = DataBase.GetJourneyById(journey.Id);

                List<Day> tempDays = DataBase.GetDaysBetweenDates(temp.IniDate, temp.EndDate);
                List<Day> newDays = DataBase.GetDaysBetweenDates(journey.IniDate, journey.EndDate);

                List <Day> deleteDays = tempDays.FindAll( x => !newDays.Contains(x) );

                if ( deleteDays.Find(x => x.Events.Count != 0 || x.Entries.Count != 0) != null )
                {
                    bool check = await Alerter.AlertDayInfoWillBeLost();
                    if (!check) return false;
                } 

                DataBase.DeleteDays(deleteDays);

                int duration = (int)(journey.EndDate - journey.IniDate).TotalDays + 1;
                Enumerable.Range(0, duration).
                    Where(i => !temp.Days.Contains(new Day(journey.IniDate.AddDays(i)))).
                    Select(i => new Day(journey.IniDate.AddDays(i)) { Journey = temp, JourneyId = temp.Id }).
                    ToList().
                    ForEach(i => 
                        {
                            DataBase.InsertDay(i);
                            temp.Days.Add(i); 
                        });

                temp.IniDate = journey.IniDate;
                temp.EndDate = journey.EndDate;

                DataBase.UpdateJourney(temp);
            }
            else
            {
                await Alerter.AlertDatesAlreadyInUse();
            }
            return true;
        }

        public static bool SaveImage(ImageModel image)
        {
            Image temp = ImageFromModel(image);

            if (!image.Path.Equals(temp.Path)) temp.Path = image.Path;
            if (!image.Caption.Equals(temp.Caption)) temp.Caption = image.Caption;

            DataBase.UpdateImage(temp);
            return true;
        }

        public static bool SaveEntry(EntryModel entry, DateTime daySelected)
        {
            Entry temp = EntryFromModel(entry);
            Day day = temp.Day;

            if (!entry.Title.Equals(temp.Title)) temp.Title = entry.Title;
            if (!temp.Day.Date.Equals(daySelected.Date))
            {
                Day newDay = DataBase.GetDayFromDate(daySelected);
                day.Entries.Remove(temp);
                newDay.Entries.Add(temp);

                DataBase.UpdateDay(day);
                DataBase.UpdateDay(newDay);

                temp.Day = newDay;
                temp.DayId = newDay.Id;
            }

            if (DataBase.UpdateEntry(temp)) Alerter.AlertEntrySaved();
            return true;
        }

        public static bool SaveEvent(EventModel evento)
        {
            Event temp = EventFromModel(evento);

            if (!evento.Title.Equals(temp.Title)) temp.Title = evento.Title;
            if (!evento.Time.Equals(temp.Time)) temp.Time = evento.Time;
            if (!evento.Address.Equals(temp.Address)) temp.Address = evento.Address;
            if (!evento.PhoneNumber.Equals(temp.PhoneNumber)) temp.PhoneNumber = evento.PhoneNumber;

            bool Days = evento.IniDay.Equals(temp.Days.First().Date) && evento.EndDay.Equals(temp.Days.Last().Date);

            if (!Days)
            {
                List<Day> days = DataBase.GetDaysBetweenDates(evento.IniDay, evento.EndDay);

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

        public static bool DeleteJourney(int JourneyId)
        {
            if (DataBase.DeleteJourneyById(JourneyId)) Alerter.AlertJourneyDeleted();
            return true;
        }

        public static bool DeleteEvent(EventModel Event)
        {
            Event temp = EventFromModel(Event);
            if (DataBase.DeleteEvent(temp)) Alerter.AlertEventDeleted();
            return true;
        }

        public static bool DeleteEntry(EntryModel Entry)
        {
            Entry temp = EntryFromModel(Entry);
            if (DataBase.DeleteEntry(temp)) Alerter.AlertEntryDeleted();
            return true;
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
            temp.JourneyState = journey.JourneyState;
            temp.CoverId = journey.CoverId;

            ImageModel tempI = GetImageById(journey.CoverId);
            if (tempI != null)
			{
                temp.CoverSource = tempI.ImageSour;
            } else
			{
                temp.CoverSource = new ImageModel().ImageSour;
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

            temp.Flag = CommonVariables.GetFlag(destiny.Code);

            return temp;
        }
        private static List<DestinyModel> DestiniesToModel(List<Destiny> destinies)
        {
            List<DestinyModel> temp = new List<DestinyModel>();

            foreach (Destiny destiny in destinies)
            {
                temp.Add(DestinyToModel(destiny));
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
                    temp2.Time = data.Time;

                    if (data.Image != null)
                    {
                        temp2.Path = data.Image.Path;
                        temp2.Caption = data.Image.Caption;
                    } else
                    {
                        temp2.Caption = "";
                    }
                    temp2.Journey = data.Entry.Day.Journey.Name;

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
        
        private static ImageModel ImageToModel(Image image)
        {
            ImageModel temp = new ImageModel();

            temp.Id = image.Id;

            temp.Path = image.Path;
            temp.Caption = image.Caption;
            temp.Journey = image.Journey;

            return temp;
        }
        private static Image ImageFromModel(ImageModel image) => DataBase.GetImageById(image.Id);

        private static EntryImageModel EntryImageToModel(Image image)
        {
            EntryImageModel temp = new EntryImageModel();

            temp.Id = image.Id;

            temp.Path = image.Path;
            temp.Caption = image.Caption;
            temp.Journey = image.Journey;

            return temp;
        }

        #endregion

        public static DateTime GetNextDayAvailable() => DayTracker.GetNextDayAvailable();

    }
}
