# SaaS Boilerplate (.NET 9 & Angular 18)

A professional, production-ready Full Stack SaaS Boilerplate built with **.NET 9** and **Angular 18**. This project implements modern architectural patterns and best practices to serve as a solid foundation for scalable web applications.

## 🏗️ Architecture Overview

This project follows the **Clean Architecture** pattern to ensure decoupling, testability, and maintainability.

- **Domain**: Core business logic, entities, and domain exceptions.
- **Application**: Use cases (CQRS), DTOs, Mapping, and Validation logic.
- **Infrastructure**: Data persistence (EF Core), external services, and logging.
- **WebAPI**: Entry point, middleware, and controllers.

## 🚀 Key Features & Patterns

- **Clean Architecture:** Strict separation of concerns across four main layers.
- **CQRS Pattern:** Implemented using **MediatR** for clean command and query separation.
- **Automatic Validation:** Request validation via **MediatR Pipeline Behaviors** and **FluentValidation**.
- **Global Exception Handling:** Unified JSON error responses via `IExceptionHandler`.
- **Generic Repository & Unit of Work:** Standardized data access layer.
- **Modern Frontend:** Angular 18+ featuring Signals and standalone components (Coming Soon).

## 🛣️ Roadmap

- [x] Initial project structure and Clean Architecture setup.
- [x] Generic Repository & Unit of Work pattern.
- [x] CQRS implementation with MediatR.
- [x] Global Exception Handling & Problem Details.
- [x] Automatic Validation Pipeline (FluentValidation).
- [x] Multi-tenancy Support (Shared Database strategy).
- [ ] **Next Step:** Identity Management (Login/Register/JWT).
- [ ] Subscription & Plan Management module (Stripe).

## 🛠️ Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (LocalDB included with Visual Studio)

### Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/bogadodiegoh/SaaS-Boilerplate.git
   ```
   
2. **Configure Database: The connection string is pre-configured for LocalDB in src/SaaS.WebApi/appsettings.json. Update it if you use a full SQL Server instance.**

3. **Apply Migrations: Run the following command from the root directory to create the database:**
	```bash
	dotnet ef database update --project src/SaaS.Infrastructure --startup-project src/SaaS.WebApi
	```
	
4. **Run the API:**
	```bash
	dotnet run --project src/SaaS.WebApi
	```
	Explore the API using Swagger at https://localhost:XXXX/swagger.
	
## 🧪 Testing Multi-tenancy
To verify data isolation between tenants:
1. Open Swagger and go to POST /api/Products.
2. Click "Try it out" and add the header x-tenant-id: client-a. Create a product.
3. Repeat the process with header x-tenant-id: client-b and a different product name.
4. Go to GET /api/Products.
5. Switch the x-tenant-id header between client-a and client-b to see how the list filters automatically.
---
*Created by [Diego Bogado](https://github.com/bogadodiegoh)*
