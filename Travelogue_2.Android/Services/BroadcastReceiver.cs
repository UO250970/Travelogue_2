using Android.Content;

namespace Travelogue_2.Droid.Services
{
	class Receiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // Do stuff here.

            string value = intent.GetStringExtra("key");
        }
    }
}