using Android.App;
using Android.Content;
using Travelogue_2.Droid.Services;
using Travelogue_2.Main.Services;

[assembly: Xamarin.Forms.Dependency(typeof(LocalNotifications))]
namespace Travelogue_2.Droid.Services
{
	class LocalNotifications : ILocalNotifications
	{
		public LocalNotifications()
		{
		}

		#region ILocalNotifications implementation

		public void SendLocalNotification(string title, string description, int iconID)
		{
			// Instantiate the builder and set notification elements:
			Notification.Builder builder = new Notification.Builder(Application.Context)
				.SetContentTitle(title)
				.SetContentText(description)
				.SetSmallIcon(Resource.Drawable.icon_about);

			// Build the notification:
			Notification notification = builder.Build();

			// Get the notification manager:
			NotificationManager notificationManager =
				Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

			// Publish the notification:
			const int notificationId = 0;
			notificationManager.Notify(notificationId, notification);
		}

		#endregion
	}
}