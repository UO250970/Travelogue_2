using Android.App;
using Android.Content;
using System;
using Travelogue_2.Main.Services;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;
using RemoteInput = AndroidX.Core.App.RemoteInput;

[assembly: Dependency(typeof(Travelogue_2.Droid.Services.Notifications))]
namespace Travelogue_2.Droid.Services
{
    public class Notifications : INotifications
    {
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";

        public const string TitleKey = "Title";
        public const string MessageKey = "Message";
        public const string TypeKey = "Type";
        public const string TypeReply = "Reply";
        public const string TypePhoto = "Photo";

        public const string KEY_TEXT_REPLY = "key_text_reply";

        bool channelInitialized = false;
        int messageId = 0;
        int replyPendingIntentId = 0;
        int photoPendingIntentId = 0;

        NotificationManager manager;

        public event EventHandler NotificationReceived;

        public static Notifications Instance { get; private set; }

        public Notifications() => Initialize();

        public void Initialize()
        {
            if (Instance == null)
            {
                CreateNotificationChannel();
                Instance = this;
            }

            //var service = new Intent(AndroidApp.Context, typeof(ReceiverService));
            //AndroidApp.Context.StartService(service);

            /*var temp2 = AndroidApp.Context;
            AndroidApp.Context.StartService(service2);*/
        }

        public void SendNotification(string title, string message)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }
            Show(title, message);
        }

        RemoteInput remoteEntryInput = new RemoteInput.Builder(KEY_TEXT_REPLY)
                .SetLabel("Escribir entrada")
                .Build();

        private Intent replyIntent;
        private Intent photoIntent;

        public void Show(string title, string message)
        {
            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.abc_ab_share_pack_mtrl_alpha))
                .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            NotificationCompat.Action reply = CreateReplyIntent();
            builder.AddAction(reply);

            NotificationCompat.Action image = CreateImageIntent();
            builder.AddAction(image);

            Notification notification = builder
                .SetOngoing(true)
                .Build();
            manager.Notify(messageId++, notification);
        }

        NotificationCompat.Action CreateReplyIntent()
        {
            replyIntent = new Intent();
            int requestCode = replyPendingIntentId++;

            PendingIntent replyPendingIntent;
            if ((int)Build.VERSION.SdkInt >= 24)
            {
                replyIntent = new Intent(AndroidApp.Context, typeof(MainActivity))
                        .AddFlags(ActivityFlags.IncludeStoppedPackages)
                        .SetAction(MainActivity.REPLY_ACTION)
                        .PutExtra(MainActivity.REQUEST_CODE, requestCode)
                        .PutExtra(TypeKey, TypeReply);

                replyPendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, requestCode, replyIntent, PendingIntentFlags.UpdateCurrent);
            }
            else
            {
                replyIntent = new Intent(AndroidApp.Context, typeof(MainActivity));
                replyIntent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask)
                            .PutExtra(TypeKey, TypeReply);

                replyPendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, requestCode, replyIntent, PendingIntentFlags.UpdateCurrent);
            }

            // Create the reply action and add the remote input.
            return new NotificationCompat.Action.Builder(Resource.Drawable.abc_ab_share_pack_mtrl_alpha,
                            "Escribir entrada", replyPendingIntent)
                            .AddRemoteInput(remoteEntryInput)
                            .Build();
        }

        NotificationCompat.Action CreateImageIntent()
        {
            photoIntent = new Intent(AndroidApp.Context, typeof(MainActivity))
                        .SetAction(MainActivity.PHOTO_ACTION)
                        .PutExtra(TypeKey, TypePhoto);

            // Build a PendingIntent for the reply action to trigger.
            PendingIntent phtoPendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, photoPendingIntentId++, photoIntent, PendingIntentFlags.UpdateCurrent);

            return new NotificationCompat.Action.Builder(Resource.Drawable.abc_ab_share_pack_mtrl_alpha,
                            "Sacar foto", phtoPendingIntent)
                            .Build();
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