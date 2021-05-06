using NUnit.Framework;
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
            app = AppInitializer.StartApp(platform);
        }

        /** Navegación básica por las distintas lengüetas del menú */
        [Test]
        public void MenuNavigationTest()
        {
            app.Repl();
        }
    }
}
