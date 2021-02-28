namespace Travelogue_2.Main.Services
{
	public interface ILocalNotifications
	{
		void SendLocalNotification(string title, string description, int iconID);
	}
}
