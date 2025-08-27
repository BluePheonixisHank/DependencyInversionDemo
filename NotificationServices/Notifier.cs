using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices;


// A high-level module responsible for sending notifications.
// It depends on the IMessageService abstraction, not on concrete implementations.

public class Notifier
{
    private readonly IMessageService _messageService;

    // Initializes a new instance of the Notifier class.
    public Notifier(IMessageService? messageService)
    {
        // This guard clause correctly handles the null case.
        _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
    }

    // Sends a notification using the injected message service.
    public void SendNotification(string? message)
    {
        // This guard clause correctly handles the null or empty case.
        if (string.IsNullOrEmpty(message))
        {
            throw new ArgumentException("Message cannot be null or empty.", nameof(message));
        }

        _messageService.Send(message);
    }
}
