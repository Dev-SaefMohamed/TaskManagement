Task Management API
A .NET 8 Web API project built with Clean Architecture to manage simple task records. Implements a layered structure with Core, Infrastructure, Application, and Api projects. Includes Entity Framework Core, Repository pattern, Dependency Injection, and full CRUD operations.

Table of Contents
Project Structure

Requirements

How to Run (Visual Studio or CLI)

Database Configuration

API Endpoints

Filtering & Sorting (Bonus)

Unit Tests (Bonus)

Notes & Deployment Tips

Author

1. Project Structure
TaskManagement.sln

TaskManagement.Core

Entities

Interfaces

TaskManagement.Infrastructure

Data

Repositories

TaskManagement.Application

DTOs

Interfaces

Services

TaskManagement.Api

Controllers

Program.cs

appsettings.json

2. Requirements
Visual Studio 2022 or newer with ASP.NET workload

.NET 8 SDK

SQL Server LocalDB or SQLite

Postman or any REST client for API testing

3. How to Run
A. Run using Visual Studio
Open TaskManagement.sln..

Set TaskManagement.Api as the startup project.

Update the connection string in appsettings.json if needed.

Open Package Manager Console (Tools → NuGet Package Manager → Package Manager Console).

Set the default project to TaskManagement.Infrastructure.

Run the following commands:

Add-Migration InitialCreate -Project TaskManagement.Infrastructure -StartupProject TaskManagement.Api

Update-Database -Project TaskManagement.Infrastructure -StartupProject TaskManagement.Api

Run the project (F5 or Ctrl+F5) and open Swagger at https://localhost:{port}/swagger

B. Run using CLI
dotnet restore

dotnet ef migrations add InitialCreate -p TaskManagement.Infrastructure -s TaskManagement.Api

dotnet ef database update -p TaskManagement.Infrastructure -s TaskManagement.Api

dotnet run --project TaskManagement.Api

4. Database Configuration
SQL Server LocalDB example: Connection string → Server=(localdb)\mssqllocaldb;Database=TaskMgmtDb;Trusted_Connection=True;

SQLite example: Connection string → Data Source=taskmgmt.db

If using SQLite, install Microsoft.EntityFrameworkCore.Sqlite and update Program.cs to use UseSqlite.

5. API Endpoints
GET /api/tasks → Get all tasks

GET /api/tasks/{id} → Get task by ID

POST /api/tasks → Create a new task

PUT /api/tasks/{id} → Update an existing task

DELETE /api/tasks/{id} → Delete a task

GET /api/tasks/filter?isCompleted=false&sortBy=due → Filter and sort tasks (bonus)

POST example body: Title: "New task" Description: "Example description" DueDate: 2025-12-01T00:00:00Z

6. Filtering & Sorting (Bonus)
Supported query parameters:

isCompleted → true, false, or omit to return all

sortBy → created, due, title

Examples:

GET /api/tasks/filter?isCompleted=true

GET /api/tasks/filter?sortBy=due

GET /api/tasks/filter?isCompleted=false&sortBy=created

7. Unit Tests (Bonus)
Located in TaskManagement.Tests

Framework: xUnit

Uses InMemory EF Core for fast testing

Tests cover:

Repository CRUD operations

Business rules validation (example: DueDate must be in the future)

Run tests with: dotnet test

8. Notes & Deployment Tips
Do not push real connection strings to public repositories. Use User Secrets or environment variables.

For deployment to Azure App Service, configure the connection string in Application Settings.

To add authentication later, integrate ASP.NET Identity with JWT.

For large datasets, implement pagination and add indexes to frequently queried columns (DueDate, IsCompleted).

9. Author
Saef Mohamed Email: saefmsalah@gmail.com LinkedIn: https://www.linkedin.com/in/saef-mohamed-1968a62a4 GitHub: https://github.com/Dev-SaefMohamed

For large datasets, implement pagination and add indexes to frequently queried columns (DueDate, IsCompleted).

9. Author
Saef Mohamed
Email: saefmsalah@gmail.com
LinkedIn: https://www.linkedin.com/in/saef-mohamed-1968a62a4
GitHub: https://github.com/Dev-SaefMohamed
