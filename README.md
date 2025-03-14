# **GarageConsole**

## **Project Overview**
**GarageConsole** is an exercise completed as part of the course **"Object-Oriented Programming using the .NET Framework and C#"**. The project demonstrates **proper object-oriented system design**, emphasizing **clean and modular code** that enables easy maintenance and reuse. The system simulates a garage management application, allowing users to register, service, and manage various vehicles while implementing key **object-oriented programming (OOP)** principles.

---

## **Project Structure & Implementation**
The project is structured into multiple classes that represent vehicles, energy types, garage stations, and the main management system:
- **Inheritance and Polymorphism** – The vehicle classes are implemented hierarchically, where `Vehicle` is the base class, and specific classes like `Car`, `Motorcycle`, and `Truck` inherit from it.
- **Collections Usage** – The system utilizes **Dictionary** structures to manage vehicle records in the garage, using a unique identifier for each vehicle.
- **Enums Implementation** – The project employs `enum` types to represent vehicle statuses, energy types, and license categories.
- **DLL Usage (External Assembly)** – Some logic is separated into an external library for better code separation and reusability across different projects.
- **Multi-Project Structure** – The project is divided into multiple sub-projects within the solution, including a main application and an external library for business logic.
- **Exception Handling** – The system efficiently handles errors using `try-catch` blocks to prevent crashes and provide meaningful error messages.

---

## **Technologies & Design Patterns**
- **Programming Language**: C#
- **Development Environment**: .NET Framework
- **Design Patterns Used**:
  - **Factory Method** – For creating instances of different vehicle types based on user input.
  - **Exception Handling** – A structured approach to error management.

---

The project showcases **well-structured object-oriented system design**, focusing on **clean and maintainable code** that ensures long-term usability and scalability. 🚗🔧

