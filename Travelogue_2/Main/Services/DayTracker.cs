using Plugin.Settings;
using System;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.BBDD;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Travelogue_2.Main.Services
{
    public static class DayTracker
    {
        private static DateTime LastDay;
        public static DateTime GetLastDay() { return LastDay; }
        public static void SetLastDay(DateTime day)
        {
            LastDay = day;
            CrossSettings.Current.AddOrUpdateValue(nameof(LastDay), LastDay.Date);
        }

        public static string CurrentJourneyId = string.Empty;

        private static bool OnTrack;
        public static bool IsJourneyOnTrack() { return OnTrack; }
        private static void SetOnTrack(bool newOnTrack)
        {
            OnTrack = newOnTrack;
            CrossSettings.Current.AddOrUpdateValue(nameof(OnTrack), OnTrack);
        }


        private static int JourneyOnTrackId;
        private static Journey JourneyOnTrack;
        public static Journey GetJourneyOnTrack() { return JourneyOnTrack; }
        private static void SetJourneyOnTrack(Journey journey)
        {
            if (journey == null)
			{
                CrossSettings.Current.AddOrUpdateValue(nameof(JourneyOnTrackId), 0);
            }
			else
            {
                JourneyOnTrack = journey;
                CrossSettings.Current.AddOrUpdateValue(nameof(JourneyOnTrackId), JourneyOnTrack.Id);
            }
        }


        private static int CurrentDay;
        public static int GetCurrentDay() { return CurrentDay; }
        private static void SetCurrentDay(int currentDay)
        {
            CurrentDay = currentDay;
            CrossSettings.Current.AddOrUpdateValue(nameof(CurrentDay), CurrentDay);
        }


        private static int TotalDays;
        public static int GetTotalDays() { return TotalDays; }
        private static void SetTotalDays(int totalDays)
        {
            TotalDays = totalDays;
            CrossSettings.Current.AddOrUpdateValue(nameof(TotalDays), TotalDays);
        }


        private static DateTime NextDayAvailable;
        public static DateTime GetNextDayAvailable()
        {
            FindNextDayAvailable();
            return NextDayAvailable;
        }
        private static void SetNextDayAvailable(DateTime nextDayAvailable)
        {
            NextDayAvailable = nextDayAvailable;
            CrossSettings.Current.AddOrUpdateValue(nameof(NextDayAvailable), NextDayAvailable.Date);
        }

        public static void Start()
        {
            Properties();
            UpdateDataBase();
            if (IsJourneyOnTrack()) { UpdateDate(); }
            SetLastDay(DateTime.Today);

            CheckInitialTabAsync();
        }

        /** Inicializa las propiedades persistentes en caso de que no existan
         * y las recoge si existen */
        private static void Properties()
        {
            SetLastDay(CrossSettings.Current.GetValueOrDefault(nameof(LastDay), DateTime.Today).ToLocalTime().Date);
            SetOnTrack(CrossSettings.Current.GetValueOrDefault(nameof(OnTrack), false));
            var JourneyOnTrack_Id = CrossSettings.Current.GetValueOrDefault(nameof(JourneyOnTrackId), 0);
            SetJourneyOnTrack(DataBase.GetJourneyById(JourneyOnTrack_Id));
            SetCurrentDay(CrossSettings.Current.GetValueOrDefault(nameof(CurrentDay), 0));
            SetTotalDays(CrossSettings.Current.GetValueOrDefault(nameof(TotalDays), 0));
            SetNextDayAvailable(CrossSettings.Current.GetValueOrDefault(nameof(NextDayAvailable), DateTime.Today).ToLocalTime().Date);
        }

        /** Actualiza los estados de los viajes en la base de datos 
         *  Por cada viaje recogido, si su estado no es ni OPEN ni CLOSED
         *      Si ya ha terminado, se cierra
         *      Si empieza hoy o ya ha empezado, se abre
         *  -Contract.ensures = Tras updatear la base de datos, el número de viajes con estado
         *                      OPEN es 0 o 1.*/
        public static void UpdateDataBase()
        {
            DataBase.GetJourneis().ForEach(x =>
            {
                if (!x.HasStarted() && !x.HasFinished())
                {
                    if (x.FinishedAlready()) { CloseJourney(x); }
                    else if ((x.StartsToday() || x.StartedAlready()) && !x.JourneyState.Equals(State.OPEN)) { OpenJourney(x); }
                }
                else if (x.HasStarted() && !x.HasFinished() && x.FinishedAlready()) { CloseJourney(x); }
            });

            //Assert.IsFalse(DataBase.GetJourneis().Where(x => x.HasStarted() == true).Count() > 1);
        }

        /** Método llamado desde la creación de viajes, 
         * con estado CREATED, si empieza hoy se inicia el viaje */
        public static void UpdateJourney(Journey journey)
        {
            if (!journey.HasStarted() && !journey.HasFinished())
            {
                if (journey.FinishedAlready()) { CloseJourney(journey); }
                else if ((journey.StartsToday() || journey.StartedAlready()) && !journey.JourneyState.Equals(State.OPEN)) { OpenJourney(journey); }
            }
            else if (journey.HasStarted() && !journey.HasFinished() && journey.FinishedAlready()) { CloseJourney(journey); }
        }

        static INotifications notificationManager = DependencyService.Get<INotifications>();

        /** Comprueba si se el viaje que se le pasa se inicia hoy, y en caso
         * afirmativo lo inicia
         * -Contrat.ensures = El viaje guardado en JourneyOnTrack DEBE ser null y su estado no puede ser
         *                      OPEN ni CLOSED*/
        private static async void OpenJourney(Journey journey)
        {
            //Assert.AreEqual(GetJourneyOnTrack(), null ); - puede venir de UpdateDataBase, y en ese caso puede haber otro journey sin cerrar

            journey.StartJourney();
            DataBase.UpdateJourney(journey);

            SetOnTrack(true);
            SetJourneyOnTrack(journey);

            int currentDay = (int)(DateTime.Today - journey.IniDate).TotalDays + 1;
            SetCurrentDay(currentDay);

            SetTotalDays(journey.Duration());

            CheckInitialTabAsync();

            notificationManager.SendNotification("¡Estamos de viaje!", "Desliza si quieres añadir una entrada...");
        }

        private static void DelayJourney(Journey journey) // TODO Añadir al testeo
        {
            journey.EndDate.AddDays(1);
            journey.IniDate.AddDays(1);
            foreach (Day day in journey.Days)
            {
                day.Date.AddDays(1);
            }
        }

        /** Comprueba si se el viaje que se le pasa ha pasado la fecha final, y en caso
         * afirmativo lo cierra. Al cerrarse, se reinicia las variables de propiedades.*/

        // TODO metodo modificado posterior a test, volver a testear
        private static async void CloseJourney(Journey journey)
        {
            if (true
                //await Alerter.AlertJourneyCanBeClosed()
                )
            {
                if (GetJourneyOnTrack() != null && GetJourneyOnTrack().Id == journey.Id) ReStart();

                journey.FinishJourney();
                if (true) //   journe    y.Journal is null)
                {
                    //Journal journal = new Journal(journey);
                    //DataBase.InsertJournal(journal);
                }
                DataBase.UpdateJourney(journey);

                CheckInitialTabAsync();
            }
        }

        private static void ReStart()
        {
            SetLastDay(DateTime.Today);
            SetOnTrack(false);
            SetJourneyOnTrack(null);

            SetCurrentDay(0);
            SetTotalDays(0);

            CheckInitialTabAsync();
        }

        /** Actualiza el día actual del viaje. Si el día actual es superior
         * al total de días, se reinicia el tracker*/
        internal static void UpdateDate()
        {
            DateTime temp = GetLastDay();
            SetLastDay(DateTime.Today);

            //Si es distinto día, se suma al currentDay
            if (temp.CompareTo(LastDay) != 0)
            {
                int currentDay = GetCurrentDay() + (int)(LastDay - temp).TotalDays + 1;
                SetCurrentDay(currentDay);
            }

            //Si el current llega al final, se reinicia todo
            if (CurrentDay > TotalDays) { ReStart(); }
        }

        // Retorna el siguiente día disponible para iniciar un viaje
        private static void FindNextDayAvailable()
        {
            DateTime LastDay = OnTrack ? GetJourneyOnTrack().EndDate.Date : DateTime.Today;

            do
            {
                LastDay = LastDay.AddDays(1);
            }
            while (!DataBase.CheckDateIsEmpty(LastDay));

            SetNextDayAvailable(LastDay);

            //Si no hay ninguno en track, significa que no hay ninguno que empiece hoy, 
            //pero no es una obligación empezar uno que tecnicamente empiece hoy...
        }

        public static async Task CheckInitialTabAsync()
        {
            if (IsJourneyOnTrack()) { await Shell.Current.GoToAsync("//JourneyOngoingView"); }
            else { await Shell.Current.GoToAsync("//LibraryView"); }
        }
    }

}
