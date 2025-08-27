# Notification Service: A Dependency Inversion Principle Demo

## 1. Introduction

This project is a C#/.NET application that provides a clear and practical demonstration of the **Dependency Inversion Principle (DIP)**. It implements a simple notification service where the delivery method (Email, SMS, etc.) can be swapped without changing the core application logic, resulting in a flexible, decoupled, and highly testable design.

## 2. Design and Principle

### The Dependency Inversion Principle (DIP)

This principle states that high-level modules (which contain business logic) should not depend on low-level modules (which handle technical details). Instead, both should depend on a shared abstraction (like an interface). This "inverts" the typical dependency flow, decoupling the components and making the system more flexible.

### How it is Implemented

The project's architecture is built entirely around this idea:

-   The **`Notifier` class** is the **high-level module** containing business logic. It has no knowledge of how messages are sent.
-   The **`EmailService`** and **`SmsService`** are the **low-level modules** that handle the specific details of sending a message.
-   The **`IMessageService` interface** is the central **abstraction**.

The `Notifier` and the concrete services both depend on the `IMessageService` abstraction. This decoupling is achieved by injecting a chosen service into the `Notifier`'s constructor. The **`Program.cs`** file acts as the **Composition Root**, where the high-level and low-level components are connected.

## 3. Testing Strategy

The project uses a focused **unit testing** strategy to verify the `Notifier` class in complete **isolation**.

A `MockMessageService` is used as a test double, allowing us to check the `Notifier`'s behavior without using any real services. The test suite provides full coverage by verifying:
1.  The primary success path (a valid message is sent).
2.  Input validation (handling of `null` and empty messages).
3.  Constructor robustness (handling of a `null` dependency).

## 4. How to Run and Verify

To run the project and its tests, open a terminal in the root directory and execute the following commands:

1.  **Build the project:**
    ```shell
    dotnet build
    ```

2.  **Run the demonstration app:**
    ```shell
    dotnet run --project NotificationApp
    ```

3.  **Run the unit tests:**
    ```shell
    dotnet test
    ```
    All 4 tests should pass successfully.

## 5. Coding Standards

This project uses an `.editorconfig` file to ensure a consistent and clean code style across the entire solution.

**To verify that all files comply with these rules**, run the following command. It will report an error if any file has formatting issues.

```shell
dotnet format --verify-no-changes