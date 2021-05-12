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
                 .ApkFile(@"C:\Users\lmendezl\AppData\Local\Xamarin\Mono for Android\Archives\2021-05-12\Travelogue_2.Android 5-12-21 9.09 AM.apkarchive\com.companyname.travelogue_2.apk")
                 .DeviceSerial("6PQ0217821002256")
                 .PreferIdeSettings()
                 .EnableLocalScreenshots()
                 .StartApp();
        }

        /** Navegación básica por las distintas lengüetas del menú */
        [Test]
        public void MenuNavigationTest()
        {
            app.Back();
            //app.Repl();
            app.WaitForElement("CreatedJourneysId", timeout: TimeSpan.FromSeconds(100));
         
            Assert.IsNotEmpty( app.Query("CreatedJourneysId") );
            //app.Tap(x => x.Marked("MediaViewButton"));
        }
    }
}
