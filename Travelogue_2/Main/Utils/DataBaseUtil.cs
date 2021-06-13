using Plugin.Settings;
using System;
using System.Collections.Generic;
using Travelogue_2.Main.BBDD;
using Travelogue_2.Main.Models;
using Xamarin.Forms;

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
            Destiny destiny = DataBase.GetDestinyByName("Australia");

            DataBase.UpdateJourney(jour);
            DataBase.UpdateDestiny(destiny);
            //destiny.Journeis.Add(journey);
            //journey.Countries.Add(country);
        }

        public static DestinyModel GetDestinyByName(string name) 
        {
            Destiny destiny = DataBase.GetDestinyByName(name);
			return DestinyToModel(destiny);
        }

        #region Models

        private static JourneyModel JourneyToModel(Journey journey)
		{
            JourneyModel temp = new JourneyModel();
            temp.Id = journey.Id;
            temp.Name = journey.Name;
            //temp.Image = ImageSource.FromFile(journey.Cover.Path);
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

        #endregion
    }
}
