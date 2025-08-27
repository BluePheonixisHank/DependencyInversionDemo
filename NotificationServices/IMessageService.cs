namespace NotificationServices;


// Defines a contract for services that can send messages.
// This is the central abstraction of our design.

public interface IMessageService
{
    void Send(string message);
}
