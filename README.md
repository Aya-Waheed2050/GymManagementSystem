🏋️ Gym Management System (ASP.NET Core MVC)

📌 Project Overview

This project is a Gym Management System built using ASP.NET Core MVC, focused on backend architecture, business rules enforcement, and real-world system constraints.

The application follows Clean Architecture principles, ensuring scalability, maintainability, and clear separation of concerns between Presentation, Business Logic, and Data Access layers.


---

🧩 Implemented Modules

🔹 Trainer Management

Full CRUD operations

Email and phone uniqueness validation

Prevent deletion of trainers assigned to active or upcoming sessions

Dedicated ViewModels for Create, Update, and Details views


🔹 Session Management

Create, update, and delete training sessions

Enforced business rules:

Valid trainer and category existence

Future start dates only

Capacity validation (0–25)


Restrictions on updating or deleting sessions with bookings or upcoming schedules

Real-time calculation of available slots

Automatic session status detection (Upcoming / Ongoing / Completed)


🔹 Membership Plan Management

View and update membership plans

Soft delete / activation toggle

Prevent updates or deactivation when active memberships exist


🔹 Account & Admin Management

Authentication and authorization using ASP.NET Core Identity

Role-Based Access Control (Admin / SuperAdmin)

Secure login and logout flows

Admin management (list, register, delete)

Custom Access Denied page for unauthorized access attempts



---

🏗 Architecture & Design Patterns

Clean Architecture (Presentation, Business Logic, Data Access)

Service Layer for business rules and validation

Unit of Work & Generic Repository patterns

AutoMapper for model-to-view separation



---

🛠 Technologies Used

ASP.NET Core MVC

Entity Framework Core (Code First)

SQL Server

ASP.NET Core Identity

AutoMapper

LINQ

Razor Views & Partial Views

Data Annotations & Client-Side Validation



---

▶️ Getting Started

1. Clone the repository


2. Update the connection string in appsettings.json


3. Apply migrations and update the database


4. Run the project using Visual Studio or dotnet run




---

🚀 Future Enhancements

Member subscription and attendance tracking

Online session booking and cancellation

Payment integration for membership plans

Reporting and analytics dashboard

RESTful API version for mobile or SPA integration



---

🎯 Key Focus

This project emphasizes backend development, architecture-driven design, and business rule enforcement, making it a strong foundation for a production-ready gym management platform.
