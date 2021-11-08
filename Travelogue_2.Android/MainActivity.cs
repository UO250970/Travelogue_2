using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using global::Xamarin.Forms;
using Xamarin.Essentials;
using Syncfusion.XForms.Android.PopupLayout;

namespace Travelogue_2.Droid
{
    [Activity(Label = "Travelogue", Icon = "@mipmap/icon", Theme = "@style/MainTheme",
        MainLauncher = false,
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const string REPLY_ACTION = "com.xamarin.directreply.REPLY";
        public const string PHOTO_ACTION = "com.xamarin.directreply.REPLY";
        public const string REQUEST_CODE = "request_code";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            SfPopupLayoutRenderer.Init();

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            //var temp = DependencyService.Get<ReceiverService>();
            //temp.broadcastReceiver.OnReceive(this, intent);
            base.OnNewIntent(intent);

        }

    }
}