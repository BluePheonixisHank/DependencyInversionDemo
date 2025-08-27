using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices;


// A low-level module that implements IMessageService to send push notifications.
public class PushNotificationService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending push notification: {message}");
    }
}
