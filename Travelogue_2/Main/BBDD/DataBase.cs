﻿using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Travelogue_2.Main.Models;

namespace Travelogue_2.Main.BBDD
{
    public static class DataBase
    {
        private static SQLiteConnection conn;
        private static readonly string Db_name = "Travelogue.db3";
        private static string Folder;

        public static void OpenDataBase(string folder)
        {
            Folder = folder;
            conn = GetConnection();
            // Para Tests
            //DropDataBase();
            CreateDatabase();
        }

        private static SQLiteConnection GetConnection()
        {
            conn = new SQLiteConnection(System.IO.Path.Combine(Folder, Db_name), false);
            System.Diagnostics.Debug.WriteLine("Path : " + Folder);

            return conn;
        }

        public static void CloseDataBase()
        {
            conn.Dispose();
        }

        private static dynamic QueryFunc(Func<dynamic> func)
        {
            try
            {
                using (GetConnection())
                {
                    dynamic result = func();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en base de datos: " + ex.StackTrace);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        private static bool QueryAct(Action act)
        {
            try
            {
                using (GetConnection())
                {
                    act();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en base de datos: " + ex.StackTrace);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool CreateDatabase()
        {
            void Act()
            {
                conn.CreateTable<JourneyDestiny>();
                conn.CreateTable<Journey>();
                conn.CreateTable<Journal>();
                conn.CreateTable<Destiny>();
                conn.CreateTable<Embassy>();
                conn.CreateTable<DayEvent>();
                conn.CreateTable<Day>();
                conn.CreateTable<Event>();
                conn.CreateTable<Entry>();
                conn.CreateTable<EntryData>();
                conn.CreateTable<Image>();

                conn.CreateTable<Style>();
                conn.CreateTable<Card>();
            }
            return QueryAct(Act);
        }
        public static bool DropDataBase()
        {
            void Act()
            {
                conn.DropTable<JourneyDestiny>();
                conn.DropTable<Journey>();
                conn.DropTable<Journal>();
                conn.DropTable<Destiny>();
                conn.DropTable<Embassy>();
                conn.DropTable<DayEvent>();
                conn.DropTable<Day>();
                conn.DropTable<Event>();
                conn.DropTable<Entry>();
                conn.DropTable<EntryData>();
                conn.DropTable<Image>();

                conn.DropTable<Style>();
                conn.DropTable<Card>();
            }
            return QueryAct(Act);
        }

        #region Journey

        public static Journey GetJourneyById(int id)
        { // Igual este con solo get y el cascade me ahorra el find => No, porque puede no haber journeys
            Journey Func() => conn.FindWithChildren<Journey>(id, recursive: true);
            return QueryFunc(Func);
        }

        public static List<Journey> GetJourneys(State state)
        {
            List<Journey> Func() => conn.GetAllWithChildren<Journey>(x => x.JourneyState.Equals(state), recursive: true);
            List<Journey> result = QueryFunc(Func);
            return result ?? new List<Journey>();
        }

        public static List<Journey> GetJourneys()
        {
            List<Journey> Func() => conn.GetAllWithChildren<Journey>(recursive: false);
            List<Journey> result = QueryFunc(Func);
            return result ?? new List<Journey>();
        }

        public static Image GetCoverFromJourney(int journeyId)
        {
            Image Func() => conn.FindWithChildren<Journey>(journeyId, recursive: true).Cover;
            return QueryFunc(Func);
        }

        internal static string GetNameFromJourney(string journeyId)
        {
            string Func() => conn.Find<Journey>(journeyId).Name;
            return QueryFunc(Func);
        }

        public static bool HasJourneys()
        {
            var temp = GetJourneys();
            return temp != null && temp.Count() != 0;
        }

        internal static bool InsertJourney(Journey journey)
        {
            if (FindJourney(journey) == null)
            {
                void Act() => conn.InsertWithChildren(journey, recursive: true);
                return QueryAct(Act);
            }
            else
            {
                return false;
            }
        }

        /** Find */
        // Solo para el Insert
        internal static Journey FindJourney(Journey journey)
        {
            Journey Func() => conn.FindWithChildren<Journey>(journey.Id, recursive: true);
            return QueryFunc(Func);
        }

        public static bool ExistsJourneyByName(string name)
        {
            Journey Func() => conn.FindWithQuery<Journey>("select * from Journey where name = ?", name);
            return QueryFunc(Func) == null ? false : true;
        }

        /** Update */
        public static bool UpdateJourney(Journey journey)
        {
            void Act() => conn.UpdateWithChildren(journey);
            return QueryAct(Act);
        }

        /** Checks */

        public static bool CheckDatesAreEmpty(DateTime dateIni, DateTime dateEnd, int JourneyId = -1)
        {
            List<Journey> list = GetJourneys();
            Journey jour = list?.Find(x => x.Id != JourneyId && x.Collision(dateIni, dateEnd));
            return jour == null;
        }

        /** Delete */
        public static bool DeleteJourneyById(int JourneyId)
        {
            Journey temp = GetJourneyById(JourneyId);
            void Act() => conn.Delete(temp, recursive: true);
            return QueryAct(Act);
        }
        #endregion

        #region Journal

        internal static bool InsertJournal(Journal journal)
        {
            if (FindJournal(journal) == null)
            {
                void Act() => conn.InsertWithChildren(journal, recursive: true);
                return QueryAct(Act);
            }
            else
            {
                return false;
            }
        }

        /** Find */
        // Solo para el Insert
        internal static Journal FindJournal(Journal journal)
        {
            Journal Func() => conn.FindWithChildren<Journal>(journal.Id);
            return QueryFunc(Func);
        }

        public static List<Journal> GetJournals()
        {
            List<Journal> Func() => conn.GetAllWithChildren<Journal>(recursive: false);
            List<Journal> result = QueryFunc(Func);
            return result ?? new List<Journal>();
        }

        public static Journal GetJournalById(int id)
        {
            Journal Func() => conn.FindWithChildren<Journal>(id, recursive: true);
            return QueryFunc(Func);
        }

        public static List<Journal> GetJournals(State state)
        {
            List<Journal> Func() => conn.GetAllWithChildren<Journal>(x => x.JournalState.Equals(state), recursive: true);
            List<Journal> result = QueryFunc(Func);
            return result ?? new List<Journal>();
        }

        /** Update */
        public static bool UpdateJournal(Journal journal)
        {
            void Act() => conn.UpdateWithChildren(journal);
            return QueryAct(Act);
        }


        #endregion

        #region Day

        public static List<Day> GetDays()
        {
            List<Day> Func() => conn.GetAllWithChildren<Day>();
            List<Day> result = QueryFunc(Func);
            return result ?? new List<Day>();
        }

        public static bool InsertDay(Day day)
        {
            void Act() => conn.Insert(day);
            return QueryAct(Act);
        }

        public static Day GetDayFromDate(DateTime day)
        {
            Func<Day> Func = () => conn.GetAllWithChildren<Day>().FirstOrDefault(x => x.Date.CompareTo(day.Date) == 0);
            return QueryFunc(Func);
        }

        public static List<Day> GetDaysBetweenDates(DateTime dateIni, DateTime dateEnd)
        {
            Func<List<Day>> Func = () => conn.GetAllWithChildren<Day>().FindAll(x => (x.Date.CompareTo(dateIni.Date) >= 0)
                && (x.Date.CompareTo(dateEnd.Date) <= 0));
            List<Day> result = QueryFunc(Func);
            return result ?? new List<Day>();
        }

        /** Update */
        public static bool UpdateDay(Day day)
        {
            void Act() => conn.UpdateWithChildren(day);
            return QueryAct(Act);
        }

        public static void UpdateDays(List<Day> days)
        {
            foreach (Day day in days) UpdateDay(day);
        }

        /** Delete */
        public static bool DeleteDay(Day day)
        {
            void Act() => conn.Delete(day);
            return QueryAct(Act);
        }

        public static void DeleteDays(List<Day> days)
        {
            foreach (Day day in days) DeleteDay(day);
        }

        /** Check */
        public static bool CheckDateIsEmpty(DateTime date)
        {
            List<Day> list = GetDays();
            Day day = list?.Find(x => x.Date.CompareTo(date) == 0);
            return day == null;
        }

        #endregion

        #region Event

        public static Event GetEventById(int id)
        {
            Event Func() => conn.GetWithChildren<Event>(id, recursive: true);
            return QueryFunc(Func);
        }

        public static bool InsertEvent(Event evento)
        {
            void Act() => conn.InsertWithChildren(evento);
            return QueryAct(Act);
        }

        public static bool UpdateEvent(Event evento)
        {
            void Act() => conn.UpdateWithChildren(evento);
            return QueryAct(Act);
        }

        public static bool DeleteEvent(Event evento)
        {
            void Act() => conn.Delete(evento);
            return QueryAct(Act);
        }

        #endregion

        #region Entry

        public static Entry GetEntryById(int id)
        { // Igual este con solo get y el cascade me ahorra el find => No, porque puede no haber journeys
            if (!id.Equals(0))
            {
                Entry Func() => conn.GetWithChildren<Entry>(id, recursive: true);
                return QueryFunc(Func);
            }
            else { return null; }
        }

        public static bool InsertEntry(Entry entry)
        {
            void Act() => conn.InsertWithChildren(entry, recursive: true);
            return QueryAct(Act);
        }

        public static bool UpdateEntry(Entry entry)
        {
            void Act() => conn.UpdateWithChildren(entry);
            return QueryAct(Act);
        }

        public static bool DeleteEntry(Entry entry)
        {
            void Act() => conn.Delete(entry);
            return QueryAct(Act);
        }
        #endregion

        #region EntryData

        public static EntryData GetEntryDataById(int id)
        {
            EntryData Func() => conn.FindWithChildren<EntryData>(id, recursive: true);
            return QueryFunc(Func);
        }

        public static bool InsertEntryData(EntryData entryData)
        {
            void Act() => conn.InsertWithChildren(entryData, recursive: true);
            return QueryAct(Act);
        }

        public static bool UpdateEntryData(EntryData entryData)
        {
            void Act() => conn.UpdateWithChildren(entryData);
            return QueryAct(Act);
        }

        public static bool DeleteEntryData(EntryData data)
        {
            void Act() => conn.Delete(data);
            return QueryAct(Act);
        }
        #endregion

        #region Destiny

        public static bool HasDestinies()
        {
            var temp = GetDestinies();
            return temp != null && temp.Count() != 0;
        }

        /** Insert */
        public static bool InsertDestinies(List<Destiny> countries)
        {
            void Act() => conn.InsertAllWithChildren(countries);
            return QueryAct(Act);
        }

        public static bool InsertDestiny(Destiny destiny)
        {
            void Act() => conn.Insert(destiny);
            return QueryAct(Act);
        }

        /** Get */
        public static List<Destiny> GetDestinies()
        {
            List<Destiny> Func() => conn.GetAllWithChildren<Destiny>(recursive: false);
            List<Destiny> result = QueryFunc(Func);
            return result ?? new List<Destiny>();
        }

        internal static bool GetDestiny(Destiny destiny)
        {
            void Act()
            {
                conn.GetWithChildren<Destiny>(destiny.Code, recursive: true);
            }
            return QueryAct(Act);
        }
        public static List<string> GetCountriesNames()
        {
            List<string> Func() => conn.GetAllWithChildren<Destiny>().Select(x => x.Name).ToList();
            List<string> result = QueryFunc(Func);
            return result ?? new List<string>();
        }
        public static Destiny GetDestinyByName(string destinyName)
        {
            Destiny Func() => conn.GetAllWithChildren<Destiny>().Find(x => x.Name.Equals(destinyName));
            return QueryFunc(Func);
        }
        public static List<Destiny> GetNotDefaultCountries()
        {
            Destiny Func() => conn.GetAllWithChildren<Destiny>().Find(x => x.Original == false);
            List<Destiny> result = QueryFunc(Func);
            return result ?? new List<Destiny>();
        }

        /** Update */
        public static bool UpdateDestiny(Destiny destiny)
        {
            void Act() => conn.UpdateWithChildren(destiny);
            return QueryAct(Act);
        }
        public static bool UpdateDestinies(List<Destiny> destinies)
        {
            void Act() => conn.UpdateAll(destinies);
            return QueryAct(Act);
        }

        /** Remove */
        public static bool ResetToDefaultCountries()
        {
            IEnumerable<string> destiniesIds = GetNotDefaultCountries().Select(x => x.Code);
            void Act() => conn.DeleteAllIds<Destiny>(destiniesIds);
            return QueryAct(Act);
        }
        #endregion

        #region Image

        public static List<Image> GetImagesByJourney(string journey)
        {
            List<Image> Func() => conn.GetAllWithChildren<Image>().FindAll(x => x.Journey.Equals(journey));
            List<Image> result = QueryFunc(Func);
            return result ?? new List<Image>();
        }

        internal static List<Image> GetImages()
        {
            List<Image> Func() => conn.GetAllWithChildren<Image>(recursive: false).FindAll(x => x.Journal is null);
            List<Image> result = QueryFunc(Func);
            return result ?? new List<Image>();
        }

        public static Image GetImageById(int id)
        {
            Image Func() => conn.FindWithChildren<Image>(id, recursive: true);
            return QueryFunc(Func);
        }

        public static bool InsertImage(Image image)
        {
            void Act() => conn.InsertWithChildren(image, recursive: true);
            return QueryAct(Act);
        }

        public static bool UpdateImage(Image image)
        {
            void Act() => conn.UpdateWithChildren(image);
            return QueryAct(Act);
        }

        /** Delete */
        public static bool DeleteImageById(int ImageId)
        {
            Image temp = GetImageById(ImageId);
            void Act() => conn.Delete(temp, recursive: true);
            return QueryAct(Act);
        }

        #endregion

        #region Style

        /** Get */
        public static List<Style> GetStyles()
        {
            List<Style> Func() => conn.GetAllWithChildren<Style>(recursive: false);
            List<Style> result = QueryFunc(Func);
            return result ?? new List<Style>();
        }
        public static Style GetStyleByName(string styleName)
        {
            Style Func() => conn.GetAllWithChildren<Style>().Find(x => x.Name.Equals(styleName));
            return QueryFunc(Func);
        }
        public static Style GetStyleOnUse()
		{
            Style Func() => conn.GetAllWithChildren<Style>().Find(x => x.OnUse);
            return QueryFunc(Func);
        }

        /** Insert */
        public static bool InsertStyles(List<Style> styles)
        {
            void Act() => conn.InsertAllWithChildren(styles);
            return QueryAct(Act);
        }

        public static bool HasStyles()
        {
            var temp = GetStyles();
            return temp != null && temp.Count() != 0;
        }

        /** Update */
        public static bool UpdateStyle(Style style)
		{
            void Act() => conn.UpdateWithChildren(style);
            return QueryAct(Act);
        }
        #endregion

        #region Card

        public static List<Card> GetCards()
        {
            List<Card> Func() => conn.GetAllWithChildren<Card>(recursive: false);
            List<Card> result = QueryFunc(Func);
            return result ?? new List<Card>();
        }
        public static Card GetCardById(int id)
        {
            Card Func() => conn.FindWithChildren<Card>(id, recursive: true);
            return QueryFunc(Func);
        }

        public static bool UpdateCard(Card card)
        {
            void Act() => conn.UpdateWithChildren(card);
            return QueryAct(Act);
        }

        public static bool InsertCard(Card card)
        {
            void Act() => conn.InsertWithChildren(card);
            return QueryAct(Act);
        }

        /** Delete */
        public static bool DeleteCardById(int CardId)
        {
            Card temp = GetCardById(CardId);
            void Act() => conn.Delete(temp);
            return QueryAct(Act);
        }
        #endregion

    }
}
