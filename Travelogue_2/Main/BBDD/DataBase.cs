using SQLite;
using System;

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
            //ClearDataBase();
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
                System.Diagnostics.Debug.WriteLine("Error en base de datos: " + ex.StackTrace);
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
                //conn.CreateTable<Destiny>();
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
                //conn.DeleteAll<Destiny>();
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
                //conn.DropTable<Destiny>();
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

    }
}
