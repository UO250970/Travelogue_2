using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using global::Xamarin.Forms;
using Xamarin.Essentials;
using Syncfusion.XForms.Android.PopupLayout;
using Travelogue_2.Droid.Services;
using Android.Content;

namespace Travelogue_2.Droid
{
    [Activity(Label = "Travelogue_2", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        Receiver receiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            receiver = new Receiver();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            SfPopupLayoutRenderer.Init();

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(receiver, new IntentFilter("com.xamarin.example.TEST"));
            // Code omitted for clarity
        }

        protected override void OnPause()
        {
            UnregisterReceiver(receiver);
            // Code omitted for clarity
            base.OnPause();
        }

    }
}