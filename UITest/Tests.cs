﻿using NUnit.Framework;
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
                 .ApkFile(@"C:\Users\lmendezl\AppData\Local\Xamarin\Mono for Android\Archives\2021-08-16\Travelogue_2.Android 8-16-21 3.59 PM.apkarchive\com.companyname.travelogue_2.apk")
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
            //app.Back();
            app.Repl();
        }

        [Test]
        public void ModifyEventTest()
        {
            //app.Back();
            app.Repl();
            app.Tap(x => x.Marked("StandardTrip Futur"));
            
            // TODO - Este se quitará mas adelante también
            app.Tap(x => x.Marked("Ok"));


            Assert.IsNotEmpty(app.Query("CreatedJourneysId"));

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
