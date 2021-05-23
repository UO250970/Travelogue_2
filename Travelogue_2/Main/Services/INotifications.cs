using System;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Journey;
using Xamarin.Forms;

namespace Travelogue_2.Main.Services
{
	public interface INotifications
	{
		event EventHandler NotificationReceived;
		void Initialize();
		void SendNotification(string title, string message);
	}

	public static class IReceiver
	{
		public static void ReceiveText(string text)
        {
			var temp = text = text.ToLower();
        }

		public static void ReceivePhoto()
		{
			CameraUtil.PhotoOngoingJourney();
		}
	}
}
