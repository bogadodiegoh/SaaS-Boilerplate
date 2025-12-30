# SaaS Boilerplate (.NET 9 & Angular 18)

A professional, production-ready Full Stack SaaS Boilerplate built with **.NET 9** and **Angular 18**. This project implements modern architectural patterns and best practices to serve as a solid foundation for scalable web applications.

## 🏗️ Architecture Overview

This project follows the **Clean Architecture** pattern to ensure decoupling, testability, and maintainability.

- **Domain**: Core business logic, entities, IMustHaveTenant interface, and domain exceptions.
- **Application**: Use cases (CQRS via MediatR), DTOs, Mapping, and Validation logic.
- **Infrastructure**: Data persistence (EF Core), Identity with RBAC, and multi-tenant database configuration.
- **WebAPI**: Entry point, JWT Authentication, Serilog configuration, and Global Exception Handlers.

## 🚀 Key Features & Patterns

- **Tenant Onboarding:** Automated flow to register a new Organization (Tenant) and its first Administrator in a single atomic operation.
- **Identity & RBAC:** Complete Authentication and Authorization system with Roles (Admin/User) using ASP.NET Core Identity.
- **Structured Logging:** Integrated Serilog for advanced traceability with file and console sinks.
- **CQRS Pattern:** Implemented using **MediatR** for clean command and query separation.
- **Automatic Validation:** Request validation via **MediatR Pipeline Behaviors** and **FluentValidation**.
- **Global Exception Handling:** Unified JSON error responses(Problem Details) via `IExceptionHandler` for a better Frontend experience.
- **Modern Frontend:** Angular 18+ featuring Signals and standalone components (Coming Soon).

## 🛣️ Roadmap

- [x] Initial project structure and Clean Architecture setup.
- [x] Generic Repository & Unit of Work pattern.
- [x] CQRS implementation with MediatR.
- [x] Global Exception Handling & Problem Details.
- [x] Automatic Validation Pipeline (FluentValidation).
- [x] Multi-tenancy Support (Shared Database strategy).
- [x] Identity Management (Login/Register Tenant/RBAC).
- [x] Structured Logging with Serilog.
- [x] Angular 18 Frontend implementation (Signals & Standalone).
- [x] Real-time Search & CRUD with Tenant Isolation.
- [x] Global Notification (Toast) & Loading UI System.
- [ ] Subscription & Plan Management module (Stripe).
- [ ] User Profile & Password Management.

## 🛠️ Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (LocalDB included with Visual Studio)

### Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/bogadodiegoh/SaaS-Boilerplate.git
   ```

2. **Apply Migrations: Run the following command to create the database and the new Tenants table:**
	```bash
	dotnet ef database update --project src/SaaS.Infrastructure --startup-project src/SaaS.WebApi
	```
	
3. **Run Application:**
-Backend: ```dotnet run --project src/SaaS.WebApi```
-Frontend: ``` ng serve ``` (Open in http://localhost:4200)
	
## 🧪 Testing Multi-tenancy & Security
To verify the professional onboarding and isolation flow:
1. Register a Tenant: Use POST /api/Auth/register-tenant. Provide a TenantId (e.g., tesla) and company details. This creates the organization and your Admin user.
2. Login: Use POST /api/Auth/login. You will receive a JWT token containing your tenantId and your role.
3. Authorize: Click the "Authorize" button in Swagger and enter Bearer <your_token>.
4. Create Data: Use POST /api/Products. Notice that you no longer need to send the TenantId manually; the system extracts it from your token.
5. Verify Isolation: Log in with a user from a different tenant. You will notice that the GET /api/Products endpoint only returns data belonging to your organization.
---
*Created by [Diego Bogado](https://github.com/bogadodiegoh)*
