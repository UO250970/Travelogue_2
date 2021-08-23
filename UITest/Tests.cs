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
                 .ApkFile(@"C:\Users\lmendezl\AppData\Local\Xamarin\Mono for Android\Archives\2021-08-21\Travelogue_2.Android 8-21-21 8.15 PM.apkarchive\com.companyname.travelogue_2.apk")
                 .DeviceSerial("6PQ0217821002256")
                 .PreferIdeSettings()
                 .EnableLocalScreenshots()
                 .StartApp();

            // TODO - Esto se quitará mas adelante...
            app.Tap(x => x.Marked("Ok"));
            app.Tap(x => x.Marked("Ok"));
            app.Tap(x => x.Marked("Ok"));
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

        [Test]
        public void CreateJourneyTest()
        {
            app.Repl();
        }

        [Test]
        public void ModifyEventInFuturTest()
        {
            app.Repl();
            app.WaitForElement("StandardTrip Futur", timeout: TimeSpan.FromSeconds(100));
            app.Tap(x => x.Marked("2"));

            // TODO - Este se quitará mas adelante también
            app.WaitForElement("Ok", timeout: TimeSpan.FromSeconds(100));
            app.Tap(x => x.Marked("Ok"));

            app.WaitForElement("Concierto Manin", timeout: TimeSpan.FromSeconds(100));
            app.TouchAndHold(x => x.Marked("Concierto Manin"));

            app.WaitForElement("Fecha", timeout: TimeSpan.FromSeconds(100));
            app.Tap(x => x.Marked("DateSelectorE"));

            DateTime iniDate = DateTime.Today.AddDays(3);
            DateTime endDate = DateTime.Today.AddDays(5);

            int month = endDate.Month - 1;
            int day = endDate.Day; // de primer a último día
            int year = endDate.Year;

            app.Query(x => x.Class("datePicker").Invoke("updateDate", year, month, day));
            app.WaitForElement(endDate.Date.ToString("dd/MM/yyyy"), timeout: TimeSpan.FromSeconds(100));
            app.Tap(x => x.Marked("Cancelar"));

            Assert.IsNotEmpty(app.Query(iniDate.Date.ToString("dd/MM/yyyy")));

            app.Tap(x => x.Marked("DateSelectorE"));
            app.Query(x => x.Class("datePicker").Invoke("updateDate", year, month, day));
            app.WaitForElement(endDate.Date.ToString("dd/MM/yyyy"), timeout: TimeSpan.FromSeconds(100));
            app.Tap(x => x.Marked("Aceptar"));

            Assert.IsNotEmpty(app.Query(endDate.Date.ToString("dd/MM/yyyy")));

            //Assert.IsNotEmpty(app.Query("CreatedJourneysId"));

        }

        [Test]
        public void ModifyReservationTest()
        {

        }

        [Test]
        public void ModifyEntryTest()
        {

        }
    }
}
