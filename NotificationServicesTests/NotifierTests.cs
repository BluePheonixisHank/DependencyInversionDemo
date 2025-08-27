using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotificationServices;


namespace NotificationServicesTests;

// A mock implementation of IMessageService used for testing the Notifier.
// It spies on the Notifier to see what message was passed to it.

public class MockMessageService : IMessageService
{
    public string? LastMessage { get; private set; }

    public void Send(string message)
    {
        LastMessage = message;
    }
}

[TestClass]
public class NotifierTests
{
    [TestMethod]
    public void SendNotification_WithValidMessage_ShouldCallMessageService()
    {
        // Arrange
        var mockService = new MockMessageService();
        var notifier = new Notifier(mockService);
        string testMessage = "This is a valid message.";

        // Act
        notifier.SendNotification(testMessage);

        // Assert
        Assert.AreEqual(testMessage, mockService.LastMessage);
    }

    [TestMethod]
    public void SendNotification_WithNullMessage_ShouldThrowArgumentException()
    {
        // Arrange
        var mockService = new MockMessageService();
        var notifier = new Notifier(mockService);

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => notifier.SendNotification(null));
    }

    [TestMethod]
    public void SendNotification_WithEmptyMessage_ShouldThrowArgumentException()
    {
        // Arrange
        var mockService = new MockMessageService();
        var notifier = new Notifier(mockService);

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => notifier.SendNotification(""));
    }

    [TestMethod]
    public void Constructor_WithNullMessageService_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => new Notifier(null));
    }
}
