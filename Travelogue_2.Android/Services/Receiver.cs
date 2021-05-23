using Android.App;
using Android.Content;
using Android.OS;
using System;
using Travelogue_2.Main.Services;

namespace Travelogue_2.Droid.Services
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    [IntentFilter(new[] { MainActivity.REPLY_ACTION })]
    public class Receiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.GetStringExtra(Notifications.TypeKey) == Notifications.TypeReply )
            {
                HandleReplyIntent(intent);
            }
            else if (intent.GetStringExtra(Notifications.TypeKey) == Notifications.TypePhoto)
            {
                HandlePhotoIntent();
            }
        }

        private void HandleReplyIntent(Intent intent) {
            Bundle remoteInput = RemoteInput.GetResultsFromIntent(intent);
            string temp = remoteInput?.GetCharSequence(Notifications.KEY_TEXT_REPLY);

            IReceiver.ReceiveText(temp);
        }

        private void HandlePhotoIntent()
        {
            IReceiver.ReceivePhoto();
        }
    }
}