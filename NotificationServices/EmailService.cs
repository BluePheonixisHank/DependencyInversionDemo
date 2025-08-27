using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices;

// A low-level module that implements IMessageService to send emails.
public class EmailService : IMessageService
{
    public void Send(string message)
    {
        // In a real app, this would contain logic to send an email.
        Console.WriteLine($"Sending email: {message}");
    }
}
