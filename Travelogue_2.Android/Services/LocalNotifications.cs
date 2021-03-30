using Android.App;
using Android.Content;
using System;
using Travelogue_2.Droid.Services;
using Travelogue_2.Main.Services;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(LocalNotifications))]
namespace Travelogue_2.Droid.Services
{
	class LocalNotifications : ILocalNotifications
	{
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";

        public const string TitleKey = "title";
        public const string MessageKey = "message";

        bool channelInitialized = false;
        int messageId = 0;
        int pendingIntentId = 0;
        int replyPendingIntentId = 0;
        int photoPendingIntentId = 0;

        NotificationManager manager;

        public event EventHandler NotificationReceived;

        public static LocalNotifications Instance { get; private set; }

        public LocalNotifications() => Initialize();

        public void Initialize()
        {
            if (Instance == null)
            {
                CreateNotificationChannel();
                Instance = this;
            }
        }

        public void SendNotification(string title, string message)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }
            Show(title, message);
        }

        public void ReceiveNotification(string title, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
            };
            NotificationReceived?.Invoke(null, args);
        }

        private string GetMessageText(Intent intent)
        {
            Bundle remoteInput = AndroidX.Core.App.RemoteInput.GetResultsFromIntent(intent);
            if (remoteInput != null)
            {
                return remoteInput.GetCharSequence(KEY_TEXT_REPLY);
            }
            return null;
        }

        private static readonly string KEY_TEXT_REPLY = "key_text_reply";

		AndroidX.Core.App.RemoteInput remoteEntryInput = new AndroidX.Core.App.RemoteInput.Builder(KEY_TEXT_REPLY)
                .SetLabel("Escribir entrada")
                .Build();

        private Intent intent;
        private Intent replyIntent;
        private Intent photoIntent;

        public void Show(string title, string message)
        {
            intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId++, intent, PendingIntentFlags.UpdateCurrent);

            replyIntent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);

            // Build a PendingIntent for the reply action to trigger.
            PendingIntent replyPendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, replyPendingIntentId++, replyIntent, PendingIntentFlags.UpdateCurrent);

            // Create the reply action and add the remote input.
            NotificationCompat.Action entryAction =
                    new NotificationCompat.Action.Builder(Resource.Drawable.abc_ab_share_pack_mtrl_alpha,
                            "Escribir entrada", replyPendingIntent)
                            .AddRemoteInput(remoteEntryInput)
                            .Build();

            photoIntent = new Intent("android.media.action.IMAGE_CAPTURE");

            PendingIntent phtoPendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, photoPendingIntentId++, photoIntent, PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentIntent(phtoPendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.abc_ab_share_pack_mtrl_alpha))
                .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                .AddAction(entryAction);

            Notification notification = builder.Build();
            manager.Notify(messageId++, notification);
        }

        void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }

    }
}