# Inventory Management API

A personal learning project built with ASP.NET Core to practice modern backend development techniques and patterns.

---

## Technologies & Patterns

### Framework & Language
- .NET 10.0
- C# 12
- ASP.NET Core Web API

### Architecture
- Clean Architecture (5 layers: Api, Services, Infrastructure, Core, Shared)
- CQRS Pattern with MediatR
- Repository Pattern (Generic Repository + Specific Repositories)
- Unit of Work Pattern
- Result Pattern for error handling

### Database
- PostgreSQL
- Entity Framework Core 10
- Code-First Migrations
- Entity Configuration (Fluent API)

### Security & Authentication
- JWT (JSON Web Tokens) Authentication
- Role-Based Access Control (RBAC) - Roles: Admin, Manager, Employee
- ASP.NET Identity for user management
- Password hashing with salt

### Validation
- FluentValidation
- DataAnnotations validation

### API Development
- RESTful API design
- Swagger/OpenAPI documentation
- Global Exception Handler (RFC 7807 Problem Details)
- Primary Constructor Injection

### Additional Concepts
- Dependency Injection (built-in .NET DI container)
- IOptions Pattern for configuration
- Pagination (PagedResponse)
- MediatR Pipeline Behaviors
- Generic Repository with IQueryable

---

## Project Structure

```
Inventory/
├── Api/                  # Web API layer (Controllers, Middleware, Extensions)
├── Services/             # Application layer (CQRS Commands/Queries, Handlers, Behaviors)
├── Infrastructure/       # Data layer (EF Core, Repositories, Identity, Security)
├── Core/                 # Domain layer (Entities, Business Rules, Common Types)
├── Shared/               # Cross-layer DTOs, Requests, Responses
└── Tests/                # Unit & Integration tests
```

---

## Getting Started

### Prerequisites
- .NET 10.0 SDK
- PostgreSQL

### Run the Project

```bash
# Restore packages
dotnet restore

# Run API
cd Api
dotnet run
```

Open https://localhost:5283/swagger to explore the API endpoints.

---

## What I Learned

Building this project helped me understand:

- How to structure a .NET application using Clean Architecture
- Implementing authentication and authorization with JWT
- Using MediatR for CQRS pattern and pipeline behaviors
- Working with Entity Framework Core and PostgreSQL
- Writing clean, testable code with proper separation of concerns
- Implementing global exception handling with Problem Details
- Using FluentValidation for request validation
- Managing database transactions with Unit of Work

---

## License

MIT License

---
