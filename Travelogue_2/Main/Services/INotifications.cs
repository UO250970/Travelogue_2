using System;

namespace Travelogue_2.Main.Services
{
    public interface INotifications
    {
        event EventHandler NotificationReceived;
        void Initialize();
        void SendNotification(string title, string message);
    }

    public interface IReceiver
    {

    }
}
