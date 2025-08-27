using System;
using NotificationServices;

namespace NotificationApp;

class Program
{
    static void Main()
    {
        Console.WriteLine("Notification System Demo");
        Console.WriteLine("------------------------");

        // --- Scenario 1: Sending an Email ---
        // We decide to use the EmailService.
        IMessageService emailService = new EmailService();

        // We create the Notifier and inject the chosen service via the constructor.
        // The Notifier only knows about IMessageService, not the concrete EmailService.
        var notifier = new Notifier(emailService);

        // We use the Notifier to send the message.
        Console.WriteLine("\nSending with Email Service:");
        notifier.SendNotification("Your order #123 has shipped.");


        // --- Scenario 2: Switching to SMS without changing the Notifier class ---
        // Now, we want to switch to SMS.
        IMessageService smsService = new SmsService();

        // We create a new Notifier instance with the SMS service.
        notifier = new Notifier(smsService);

        Console.WriteLine("\nSending with SMS Service:");
        notifier.SendNotification("Your package will arrive tomorrow.");


        // --- Scenario 3: Switching to Push Notification ---
        IMessageService pushService = new PushNotificationService();
        notifier = new Notifier(pushService);

        Console.WriteLine("\nSending with Push Notification Service:");
        notifier.SendNotification("Your driver is nearby.");
    }
}
