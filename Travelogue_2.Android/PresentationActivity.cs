using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;


namespace Travelogue_2.Droid
{
    [Activity(Label = "Presentation", MainLauncher = true, Theme = "@style/MyTheme.Splash")]
    public class PresentationActivity : Activity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { OnStart(); });
            startupWork.Start();
        }

        protected override void OnStart()
        {
            base.OnStart();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public override void OnBackPressed() { }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}