🏢 Elevator Control System

A desktop-based Elevator Control System built using C# and the .NET Framework.
This project demonstrates the application of Object-Oriented Programming (OOP) principles and design patterns (State & Observer) to model the logic and behaviour of a real-world elevator system.

🚀 Features

Dynamic elevator control using State Design Pattern

Real-time UI interaction and event-driven updates

Automatic door operation and timed closing mechanism

Logging system for elevator activity history

Admin controls for reviewing, clearing, and storing logs

Implementation of Black Box and White Box testing

Structured architecture following SOLID principles

🧩 Technologies Used

Language: C# (C-Sharp) – Version 10.0

Framework: .NET Framework / Windows Forms

Database: MySQL

Design Patterns: State Pattern, Observer Pattern

Tools: Visual Studio 2022, ADO.NET, GitHub

🏗️ System Architecture

The system consists of the following key components:

Elevator – Core class controlling elevator movement, door operations, and state changes.

IElevatorState – Interface defining the actions available for different states.

Concrete States – Includes IdleState, MovingState, DoorOpenState, and DoorClosedState.

DatabaseHelper – Manages log storage and retrieval from the database.

MainForm (UI) – Provides an interactive user interface for controlling and monitoring the elevator.

🧠 Object-Oriented Concepts Applied

Encapsulation: Protects elevator state and behaviour through controlled methods.

Inheritance: Shared functionality through a common state interface.

Polymorphism: Enables dynamic behaviour switching between different states.

Abstraction: Simplifies complex database operations using helper classes.

⚙️ Installation & Setup

Clone this repository:

git clone https://github.com/yourusername/ElevatorControlSystem.git


Open the project in Visual Studio.

Configure your MySQL database (update connection string in DatabaseHelper.cs).

Build and run the solution.

Use the interface to test elevator requests and admin features.

🧾 Testing

The project implements both Black Box and White Box testing:

Black Box: Verifies correct output based on user inputs (e.g., floor requests).

White Box: Validates internal methods, state transitions, and database functions.

🧍‍♂️ Author

Nirajan Dhungel
University of Bedfordshire (PCPS College)
