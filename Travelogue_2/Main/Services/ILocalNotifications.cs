using System;

namespace Travelogue_2.Main.Services
{
	public interface ILocalNotifications
	{
		event EventHandler NotificationReceived;
		void Initialize();
		void SendNotification(string title, string message);
		void ReceiveNotification(string title, string message);
	}
}
