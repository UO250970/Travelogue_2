using SQLite;
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

        public static void OpenDataBase(String folder)
        {
            Folder = folder;
            conn = GetConnection();
            // Para Tests
            DropDataBase();
            // Para Uso
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
            catch (SQLiteException ex)
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
                System.Diagnostics.Debug.WriteLine("Error en base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        private static bool CreateDatabase()
        {
            void Act()
            {
                //conn.CreateTable<JourneyCountry>();
                conn.CreateTable<Journey>();
                //conn.CreateTable<Journal>();
                conn.CreateTable<Destiny>();
                conn.CreateTable<Embassy>();
                conn.CreateTable<Day>();
                //conn.CreateTable<DayReser>();
                //conn.CreateTable<Entry>();
                //conn.CreateTable<Data>();
                //conn.CreateTable<Text_Info>();
                conn.CreateTable<Image>();
                //conn.CreateTable<Place_Info>();
                //conn.CreateTable<AbstractEvent>();
                //conn.CreateTable<Event_Info>();
                //conn.CreateTable<Reser_Info>();
            }
            return QueryAct(Act);
        }

        public static bool ClearDataBase()
        {
            void Act()
            {
                //conn.DeleteAll<JourneyCountry>();
                conn.DeleteAll<Journey>();
                //conn.DeleteAll<Journal>();
                conn.DeleteAll<Destiny>();
                conn.DeleteAll<Embassy>();
                conn.DeleteAll<Day>();
                //conn.DeleteAll<DayReser>();
                //conn.DeleteAll<Entry>();
                //conn.DeleteAll<Data>();
                //conn.DeleteAll<Text_Info>();
                conn.DeleteAll<Image>();
                //conn.DeleteAll<Place_Info>();
                //conn.DeleteAll<AbstractEvent>();
                //conn.DeleteAll<Event_Info>();
                //conn.DeleteAll<Reser_Info>();
            }
            return QueryAct(Act);
        }

        public static bool DropDataBase()
        {
            void Act()
            {
                //conn.DropTable<JourneyCountry>();
                conn.DropTable<Journey>();
                //conn.DropTable<Journal>();
                conn.DropTable<Destiny>();
                conn.DropTable<Embassy>();
                conn.DropTable<Day>();
                //conn.DropTable<DayReser>();
                //conn.DropTable<Entry>();
                //conn.DropTable<Data>();
                //conn.DropTable<Text_Info>();
                conn.DropTable<Image>();
                //conn.DropTable<Place_Info>();
                //conn.DropTable<AbstractEvent>();
                //conn.DropTable<Event_Info>();
                //conn.DropTable<Reser_Info>();
            }
            return QueryAct(Act);
        }

        #region Journey

        public static Journey GetJourneyById(int id)
        { // Igual este con solo get y el cascade me ahorra el find => No, porque puede no haber journeys
            if (!id.Equals(0))
            {
                Journey Func() => conn.GetWithChildren<Journey>(id, recursive: true);
                return QueryFunc(Func);
            }
            else { return null; }
        }

        public static List<Journey> GetJourneys(State state)
		{
            List<Journey> Func() => conn.GetAllWithChildren<Journey>(x => x.JourneyState.Equals(state), recursive: true);
            return QueryFunc(Func);
        }

        public static string HasJourneis()
        {
            string Func() => conn.GetTableInfo("Journey").Count().ToString();
            return QueryFunc(Func);
        }

        internal static bool InsertJourney(Journey journey)
        {
            if (FindJourney(journey) == null)
            {
                void Act()
                {
                    conn.InsertWithChildren(journey, recursive: true);
                }
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

            /** Update */
        public static bool UpdateJourney(Journey journey)
        {
            void Act() => conn.UpdateWithChildren(journey);
            return QueryAct(Act);
        }

		#endregion

		#region Day

        #endregion

        #region Destiny

        public static string HasDestinies()
        {
            string Func() => conn.GetTableInfo("Destiny").Count().ToString();
            return QueryFunc(Func);
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
            return QueryFunc(Func);
        }
        public static Destiny GetDestinyByName(string destinyName)
        {
            Destiny Func() => conn.GetAllWithChildren<Destiny>().Find(x => x.Name.Equals(destinyName));
            return QueryFunc(Func);
        }
        public static List<Destiny> GetNotDefaultCountries()
        {
            Destiny Func() => conn.GetAllWithChildren<Destiny>().Find(x => x.Original == false);
            return QueryFunc(Func);
        }

            /** Update */
        public static bool UpdateDestiny(Destiny destiny)
        {
            void Act() => conn.UpdateWithChildren(destiny);
            return QueryAct(Act);
        }
        public static bool UpdateDestinies(List<Destiny> countries)
        {
            void Act() => conn.UpdateAll(countries);
            return QueryAct(Act);
        }

            /** Remove */
        public static bool ResetToDefaultCountries()
        {
            IEnumerable<string> countriesIds = GetNotDefaultCountries().Select(x => x.Code);
            void Act() => conn.DeleteAllIds<Destiny>(countriesIds);
            return QueryAct(Act);
        }
        #endregion

    }
}
