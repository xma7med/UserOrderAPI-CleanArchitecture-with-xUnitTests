# User and Order Management Task – ASP.NET Core 8 (Clean Architecture)

-----> To be changed Audit service implementation to Infrastructure Layer 

This project is a lightweight sample demonstrating how to implement a clean, modular, and testable backend service using **ASP.NET Core 8**, **EF Core**, and **Clean Architecture** principles.

The system supports:

- User registration  
- Creating customer orders  
- Adding multiple order items  
- Automatic total price calculation  
- Logging audit events  
- Basic error logging  
- Unit testing using xUnit and EF Core InMemory  

---

## **Architecture Overview**

The solution follows a simple Clean Architecture structure:

Solution

--- Domain

--- Application

--- Infrastructure

--- UserAndOrderTask.APIs (Web API)

--- ProjectTest (xUnit Tests)



### **Layer Responsibilities**

| Layer | Responsibilities |
|-------|------------------|
| **Domain** | Entities, base classes, core rules |
| **Application** | DTOs, service contracts, business logic |
| **Infrastructure** | EF Core DbContext, repository logic, audit implementation |
| **APIs** | Controllers, request handling, dependency injection |
| **Tests** | Unit tests using InMemory provider |

---

##  **Unit Testing**

The project includes sample xUnit tests demonstrating:

- Total price calculation logic
- Retrieving orders after creation

To use the InMemory database in tests, make sure this package is installed:

	Microsoft.EntityFrameworkCore.InMemory

==================================================================================

--- Important Notes About Audit Logging

In this sample project, audit logging is simplified.

--- In real-world applications:

Audit fields such as:

CreatedBy

UpdatedBy

DeletedBy

Any user metadata

are not logged manually.

Instead, they are recorded automatically using:

--- IHttpContextAccessor

This allows us to access the:

Logged-in user

Their claims

JWT payload

Session context

So when using ASP.NET Core Identity or JWT Authentication, the Audit fields are filled automatically like this:

	var userId = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;


This avoids hardcoding values and ensures the audit trail is tied to the authenticated identity.

  NuGet Packages Required After Cloning

If the solution is downloaded from GitHub,
Visual Studio may require restoring missing packages.

Simply run:

dotnet restore


Or from Visual Studio:

Right-click the Solution ? Restore NuGet Packages

Required packages include (depending on your layer):

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.InMemory
BCrypt.Net-Next


Make sure all layers build successfully after the restore.

---Running the Project

Open the solution

Restore NuGet packages

Set UserAndOrderTask.APIs as Startup Project

Run with Swagger enabled

Test endpoints:

POST /api/users/register

POST /api/orders

GET /api/orders/{id}

--- Future Improvements

Integrate ASP.NET Core Identity for secure authentication

Add JWT for authorization

Automate audit logging via middleware

Implement repository abstractions

Add FluentValidation instead of DataAnnotations

Add integration tests

--- Author

This project was created as a backend coding assignment demonstration using best practices in clean architecture, modularity, and testability.

