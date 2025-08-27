## 1. Introduction

This project is a C#/.NET application designed to provide a clear, practical demonstration of the **Dependency Inversion Principle (DIP)**, one of the five foundational SOLID principles of object-oriented design.

The application implements a simple notification system that can send messages via different methods (e.g., Email, SMS, Push Notification). The core architectural goal is to allow the delivery mechanism to be changed at any time without requiring any modifications to the high-level module that initiates the notification. This creates a system that is flexible, decoupled, maintainable, and highly testable.


## 2. Core Concepts Demonstrated

*   **Dependency Inversion Principle (DIP):** The entire architecture is built around this principle.
    *   *Rule A:* High-level modules should not depend on low-level modules. Both should depend on abstractions.
    *   *Rule B:* Abstractions should not depend on details. Details should depend on abstractions.
*   **Dependency Injection (DI):** The project uses **Constructor Injection** as the mechanism to achieve Dependency Inversion. Dependencies are provided to a class through its constructor rather than being created by the class itself.
*   **Decoupled Architecture:** The high-level notification logic is completely separated from the low-level delivery mechanisms.
*   **Unit Testing in Isolation:** The solution demonstrates a robust unit testing strategy, using a mock object to test the high-level module's logic completely independently of its low-level dependencies.


## 3. Detailed Design and Architecture Analysis

The architecture of this solution is a direct and deliberate implementation of the Dependency Inversion Principle.

### The Abstraction: `IMessageService`

At the very core of this decoupled design is the `IMessageService` interface. This interface acts as a universal contract or blueprint. It defines a single, abstract capability—the ability to "Send" a message—without specifying *how* that action is performed. The abstraction is completely independent of any implementation details; it has no knowledge of emails, SMS, servers, or any other specific technology.

### The High-Level Module: `Notifier`

The `Notifier` class represents the high-level module of the system. It contains the important business logic and policies for the application. Its responsibility is to orchestrate the process of sending a notification and to enforce rules, such as ensuring that a message is not null or empty.

Critically, the `Notifier` class is designed to be completely decoupled from the low-level details. It does not know, nor does it care, whether a message is being sent via email or SMS. It depends solely on the `IMessageService` abstraction. This dependency is provided to it when it is created through a technique called constructor injection.

### The Low-Level Modules: `EmailService`, `SmsService`

The `EmailService`, `SmsService`, and `PushNotificationService` classes are the low-level modules. They are the workers that handle the specific, technical details of an implementation. Each class contains the concrete logic for its particular delivery method.

These classes fulfill their role by promising to adhere to the contract defined by the `IMessageService` interface. This relationship is what creates the "inversion" of dependency: instead of the high-level `Notifier` depending on the low-level `EmailService`, both the high-level module and the low-level module now depend on the central `IMessageService` abstraction. They both look inward toward the contract that connects them.

### The Composition Root: `Program.cs`

The `NotificationApp` project, specifically its main entry point, serves as the **Composition Root** of the application. This is the single, designated assembly point where the application's object graph is constructed. It is the only place in the entire system that has knowledge of both the high-level `Notifier` and the concrete, low-level services.

In this composition root, we make the decision of which specific service to use. It is here that we create an instance of a concrete service (like `EmailService`) and "inject" it into the `Notifier` class. This setup clearly demonstrates the ultimate flexibility of the design: if we want to switch from sending emails to sending SMS messages, the only change required is in this one specific location. The core business logic within the `Notifier` remains completely untouched.

## 4. Testing Strategy

The project employs a robust and focused **unit testing** strategy to ensure the correctness and reliability of its core business logic. The entire testing approach is designed to leverage the benefits of the Dependency Inversion Principle.

### Unit Testing in Isolation

The tests in `NotifierTests.cs` are pure **unit tests**. Their purpose is to verify the internal logic of the `Notifier` class in complete **isolation** from its dependencies (like `EmailService` or `SmsService`).

*   **Technique:** To achieve this isolation, we use a `MockMessageService` class. This mock acts as a "test double" or "spy." It implements the `IMessageService` interface, but instead of actually sending a message, it simply records the message it received. This allows our tests to verify that the `Notifier` behaved correctly without needing any real, external services.

*   **Benefit:** This approach makes the tests extremely fast, reliable, and precise. A test failure points directly to a bug in the `Notifier`'s logic, not to a problem in a downstream dependency.

### Test Case Breakdown

The test suite provides full coverage of the `Notifier` class's logical paths, including both the "happy path" and critical edge cases:

1.  **`SendNotification_WithValidMessage_ShouldCallMessageService`**
    *   **Purpose:** Verifies the primary success scenario. It ensures that when given a valid message, the `Notifier` correctly calls the `Send` method on its dependency.

2.  **`SendNotification_WithNullMessage_ShouldThrowArgumentException`**
    *   **Purpose:** Verifies input validation. It confirms that the `Notifier` correctly identifies a `null` message as invalid and throws the appropriate `ArgumentException`.

3.  **`SendNotification_WithEmptyMessage_ShouldThrowArgumentException`**
    *   **Purpose:** Verifies another input validation case. It ensures that an empty string is also correctly handled as an invalid message, throwing an `ArgumentException`.

4.  **`Constructor_WithNullMessageService_ShouldThrowArgumentNullException`**
    *   **Purpose:** Verifies the robustness of the class's construction. It ensures that the `Notifier` cannot be created in an invalid state (i.e., without a service to do its work) by confirming it throws an `ArgumentNullException`.

### How to Run Tests

To execute the full suite of tests, run the following command from the root directory of the solution:

```shell
dotnet test
```
The expected outcome is the successful execution of all 4 tests.