using NUnit.Framework;
using System;
using Xamarin.UITest;

namespace UITest
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = ConfigureApp.Android.StartApp();

            app = ConfigureApp.Android
                 .ApkFile(@"C:\Users\lmendezl\AppData\Local\Xamarin\Mono for Android\Archives\2021-10-20\Travelogue_2.Android 10-20-21 9.22 PM.apkarchive\com.companyname.travelogue_2.apk")
                 .DeviceSerial("6PQ0217821002256")
                 .PreferIdeSettings()
                 .EnableLocalScreenshots()
                 .StartApp();

            // TODO - Esto se quitará mas adelante...
            app.Tap(x => x.Marked(Variables.OkButton));
            app.Tap(x => x.Marked(Variables.OkButton));
            app.Tap(x => x.Marked(Variables.OkButton));
        }

        /** Navegación básica por las distintas lengüetas del menú */
        [Test]
        public void MenuNavigationTest()
        {
            //app.Back();
            app.Repl();
            app.WaitForElement("CreatedJourneysId", timeout: TimeSpan.FromSeconds(100));

            //Assert.IsNotEmpty( app.Query("CreatedJourneysId") );
            //app.Tap(x => x.Marked("MediaViewButton"));
        }

        #region Enter
        public void EnterLibrary() => app.Tap(x => x.Marked("NoResourceEntry-0"));
        public void EnterMedia() => app.Tap(x => x.Marked("NoResourceEntry-1"));
        public void EnterOnGoing() => app.Tap(x => x.Marked("NoResourceEntry-2"));
        public void EnterModeling() => app.Tap(x => x.Marked("NoResourceEntry-3"));
        public void EnterSettings() => app.Tap(x => x.Marked("NoResourceEntry-4"));

        public void EnterFuturTrip()
        {
            EnterLibrary();
            app.WaitForElement("StandardTrip Futur", timeout: TimeSpan.FromSeconds(100));
            app.Tap(x => x.Marked("2"));

            app.WaitForElement("Concierto Manin", timeout: TimeSpan.FromSeconds(100));
        }

        public void EnterFuturLibrary()
        {
            EnterLibrary();
            app.Tap(x => x.Marked("CreatedJourneysButton"));
            app.WaitForElement("FuturJourneisL", timeout: TimeSpan.FromSeconds(100));
        }

        public void EnterFuturLibraryTrip()
        {
            EnterFuturLibrary();
            app.WaitForElement("StandardTrip Futur", timeout: TimeSpan.FromSeconds(100));
            app.Tap(x => x.Marked("2"));

            app.WaitForElement("Concierto Manin", timeout: TimeSpan.FromSeconds(100));
        }

        public void EnterCreateFuturJourney()
        {
            EnterFuturLibrary();
            app.Tap(x => x.Marked(Variables.CreateJourneyButton));
            app.WaitForElement("CreateJourneyTitleL", timeout: TimeSpan.FromSeconds(100));
        }

        public void EnterPastTrip()
        {
            EnterLibrary();
            app.WaitForElement("StandardTrip Finished", timeout: TimeSpan.FromSeconds(100));
            app.Tap(x => x.Marked("3"));

            app.WaitForElement("Concierto Pepita", timeout: TimeSpan.FromSeconds(100));
        }

        public void EnterPastLibrary()
        {
            EnterLibrary();
            app.Tap(x => x.Marked("ClosedJourneysButton"));
            app.WaitForElement("ClosedJourneisL", timeout: TimeSpan.FromSeconds(100));
        }

        public void EnterText(string element, string text)
        {
            app.EnterText(x => x.Marked(element), text);
        }

        public void EnterPastLibraryTrip()
        {
            EnterPastLibrary();
            app.WaitForElement("StandardTrip Finished", timeout: TimeSpan.FromSeconds(100));
            app.Tap(x => x.Marked("3"));
        }
        #endregion

        #region Modify
        public void ModifyEvent(string name)
        {
            app.TouchAndHold(x => x.Marked(name));

            app.WaitForElement("Fecha", timeout: TimeSpan.FromSeconds(100));
        }

        public void ModifyReserv(string name)
        {
            app.TouchAndHold(x => x.Marked(name));

            app.WaitForElement("Fecha", timeout: TimeSpan.FromSeconds(100));
        }

        public void ModifyEntry(string name)
        {

        }

        public void ModifyEntryText(string name)
        {

        }

        public void ModifyEntryImage(string name)
        {

        }
        #endregion

        public void SaveEventButton()
        {
            app.Tap(x => x.Marked(Variables.SaveButtonE));
            app.WaitForElement(Variables.OkButton, timeout: TimeSpan.FromSeconds(100));
            app.Tap(x => x.Marked(Variables.OkButton));
        }

        public void PickDateEvent(int year, int month, int day)
        {
            app.Tap(x => x.Marked("DateSelectorE"));
            app.Query(x => x.Class("datePicker").Invoke("updateDate", year, month, day));
            app.Tap(x => x.Marked("Aceptar"));
        }

        public void PickTimeEvent(int hour, int minutes)
        {
            app.Tap(Variables.TimeSelector);
            app.Tap("toggle_mode");

            app.ClearText();
            app.EnterText(hour.ToString());

            app.Tap(x => x.Marked("input_minute"));
            app.ClearText();
            app.EnterText(minutes.ToString());

            app.Tap(Variables.AceptButton);
        }

        public void PickDestiny(string name)
        {
            app.Tap(Variables.DestinySelector);
            app.EnterText(name);
        }

        [Test]
        public void CreateJourneyTest()
        {
            app.Repl();
            EnterCreateFuturJourney();

            EnterText(Variables.TitleSelector, "Holi, test 1");

            PickDestiny("SPAIN");
        }

        // TODO Tests

        /*
         *  - Modificar settings de journey
         *      - Modificar nombre
         *      - Modificar cover
         *      - Modificar fechas
         *      - Eliminar un destino, añadir un destino
         *      - Salir y volver a entrar 
         *      - Comprobar que sigue igual
         *      
         *  - Añadir info al journey
         *      - Añadir una entry
         *      - Modificar una entry
         *      - Añadir texto a la entry
         *      - Modificar texto de la entry
         *      - Eliminar texto 
         *      - Añadir foto a una entry
         *      - Añadir pie a la foto
         *      - Añadir foto normal a un viaje
         * 
         *  - Eliminar info del journey
         *      - 
         * 
         *  - Ver info en el media
         *      - Ver journeis en el calendario
         *      - Ver fotos en el mapa
         *      - Ver fotos ordenadas por journey
         */

        /** Done */
        [Test]
        public void SeeJourneyFromLibrary()
        {
            //app.Repl();
            EnterFuturLibraryTrip();
        }

        [Test]
        public void ModifyEventTest()
        {
            //app.Repl();
            EnterFuturTrip();
            ModifyEvent("Concierto Manin");

            app.Tap(x => x.Marked(Variables.DateSelector));

            DateTime iniDate = DateTime.Today.AddDays(3);
            DateTime endDate = DateTime.Today.AddDays(5);

            int month = endDate.Month - 1;
            int day = endDate.Day; // de primer a último día
            int year = endDate.Year;

            TimeSpan time = DateTime.Now.AddHours(1).AddMinutes(5).TimeOfDay;

            Assert.IsNotEmpty(app.Query(iniDate.Date.ToString(Variables.DateFormat)));

            PickDateEvent(year, month, day);
            PickTimeEvent(time.Hours, time.Minutes);

            Assert.IsNotEmpty(app.Query(endDate.Date.ToString(Variables.DateFormat)));
            Assert.IsNotEmpty(app.Query(time.ToString(Variables.TimeFormat)));

            SaveEventButton();
            Assert.IsEmpty(app.Query("Concierto Manin"));

            app.Tap(x => x.Marked(day.ToString()));
            Assert.IsNotEmpty(app.Query("Concierto Manin"));
            Assert.IsNotEmpty(app.Query("Calle lapus"));
            Assert.IsEmpty(app.Query("Concierto Manin 2"));
            Assert.IsEmpty(app.Query("Calle lapus 1"));

            app.TouchAndHold(x => x.Marked("Concierto Manin"));

            EnterText(Variables.TitleSelector, " 2");
            EnterText(Variables.AddressSelector, " 1");
            SaveEventButton();
            app.Tap(x => x.Marked(day.ToString()));
            Assert.IsEmpty(app.Query("Concierto Manin"));
            Assert.IsEmpty(app.Query("Calle lapus"));
            Assert.IsNotEmpty(app.Query("Concierto Manin 2"));
            Assert.IsNotEmpty(app.Query("Calle lapus 1"));
        }

        [Test]
        public void ModifyReservTest()
        {
            //app.Repl();
            EnterFuturTrip();
            ModifyReserv("Hotel");

            app.Tap(x => x.Marked(Variables.DateSelector));

            DateTime iniDate = DateTime.Today.AddDays(3);
            DateTime endDate = DateTime.Today.AddDays(5);

            int month = endDate.Month - 1;
            int day = endDate.Day; // de primer a último día
            int year = endDate.Year;

            TimeSpan time = DateTime.Now.AddHours(1).AddMinutes(5).TimeOfDay;

            Assert.IsNotEmpty(app.Query(iniDate.Date.ToString(Variables.DateFormat)));

            PickDateEvent(year, month, day);
            PickTimeEvent(time.Hours, time.Minutes);

            Assert.IsNotEmpty(app.Query(endDate.Date.ToString(Variables.DateFormat)));
            Assert.IsNotEmpty(app.Query(time.ToString(Variables.TimeFormat)));

            SaveEventButton();
            Assert.IsEmpty(app.Query("Concierto Manin"));

            app.Tap(x => x.Marked(day.ToString()));
            Assert.IsNotEmpty(app.Query("Concierto Manin"));
            Assert.IsNotEmpty(app.Query("Calle lapus"));
            Assert.IsEmpty(app.Query("Concierto Manin 2"));
            Assert.IsEmpty(app.Query("Calle lapus 1"));

            app.TouchAndHold(x => x.Marked("Concierto Manin"));

            EnterText(Variables.TitleSelector, " 2");
            EnterText(Variables.AddressSelector, " 1");
            SaveEventButton();
            app.Tap(x => x.Marked(day.ToString()));
            Assert.IsEmpty(app.Query("Concierto Manin"));
            Assert.IsEmpty(app.Query("Calle lapus"));
            Assert.IsNotEmpty(app.Query("Concierto Manin 2"));
            Assert.IsNotEmpty(app.Query("Calle lapus 1"));
        }

        [Test]
        public void ModifyEntryTest()
        {
            //app.Repl();
            EnterFuturTrip();
            app.TouchAndHold(x => x.Marked("Standard"));


        }
    }

    public static class Variables
    {
        // Automation Ids
        public static string DateFormat { get => "dd/MM/yyyy"; }
        public static string TimeFormat { get => @"hh\:mm"; }

        public static string SaveButtonE { get => "SaveButtonE"; }
        public static string OkButton { get => "Ok"; }
        public static string AceptButton { get => "Aceptar"; }

        public static string TitleSelector { get => "TitleSelectorE"; }
        public static string AddressSelector { get => "AddressSelectorE"; }
        public static string DateSelector { get => "DateSelectorE"; }

        public static string DateIniSelector { get => "DateSelectorIniE"; }
        public static string DateEndSelector { get => "DateSelectorEndE"; }

        public static string TimeSelector { get => "TimeSelectorE"; }

        public static string DestinySelector { get => "DestinySelectorAC"; }

        public static string CreateJourneyButton { get => "CreateJourneyB"; }
    }
}
