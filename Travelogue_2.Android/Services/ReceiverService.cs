using Android.App;
using Android.Content;
using Android.OS;
using Plugin.Media.Abstractions;
using System;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Services;

namespace Travelogue_2.Droid.Services
{
	[Service(Label = "ReceiverService", IsolatedProcess = true)]
	public class ReceiverService : Service
	{
		public const string BROADCASTFILTER = "MainActivity.REPLY_ACTION";
		IBinder binder;
		public Receiver broadcastReceiver;

		public ReceiverService()
		{
			broadcastReceiver = new Receiver(this);
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			RegisterReceiver(broadcastReceiver, new IntentFilter(BROADCASTFILTER));
			return StartCommandResult.NotSticky;
		}

		public override IBinder OnBind(Intent intent)
		{
			binder = new ReceiverServiceBinder(this);
			return binder;
		}

		[BroadcastReceiver(Enabled = true)]
		public class Receiver : BroadcastReceiver
		{

			ReceiverService service;
			public Receiver() { }

			public Receiver(ReceiverService service) : base()
			{
				this.service = service;
			}

			public override void OnReceive(Context context, Intent intent)
			{
				if (intent.GetStringExtra(Notifications.TypeKey) == Notifications.TypeReply)
				{
					HandleReplyIntent(intent);
				}
				else if (intent.GetStringExtra(Notifications.TypeKey) == Notifications.TypePhoto)
				{
					HandlePhotoIntent();
				}
			}

			private void HandleReplyIntent(Intent intent)
			{
				Bundle remoteInput = RemoteInput.GetResultsFromIntent(intent);
				string temp = remoteInput?.GetCharSequence(Notifications.KEY_TEXT_REPLY);

				IReceiver.ReceiveText(temp);
			}

			private void HandlePhotoIntent()
			{
				MediaFile photo = IReceiver.ReceivePhoto();
                DataBaseUtil.NotificationNewPhoto(photo);
			}
		}
	}

	public class ReceiverServiceBinder : Binder
	{
		readonly ReceiverService service;

		public ReceiverServiceBinder(ReceiverService service)
		{
			this.service = service;
		}

		public ReceiverService GetReceiverService()
		{
			return service;
		}
	}
}