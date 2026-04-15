# AI Agent Guidelines for Inventory Management System

## Architecture Overview

This is a **Clean Architecture** .NET 10.0 inventory management system with CQRS pattern using MediatR. The solution follows strict separation of concerns across five layers:

- **`Core`**: Domain entities, business rules, exceptions, and common types (zero dependencies)
- **`Services`**: Application logic with CQRS (Commands/Queries), behaviors, and external service abstractions
- **`Infrastructure`**: EF Core context, repositories, migrations, and external integrations
- **`Shared`**: DTOs, requests, responses, and cross-cutting utilities
- **`Api`**: ASP.NET Core controllers, middleware, and dependency injection setup

## Key Patterns & Conventions

### CQRS Structure
- **Commands**: `Services/Features/{Feature}/Commands/{CommandName}/` containing:
  - `{CommandName}Command.cs` (request model)
  - `{CommandName}CommandHandler.cs` (business logic)
  - `{CommandName}CommandValidator.cs` (FluentValidation rules)
- **Queries**: Similar structure under `Queries/` folder
- **Result Pattern**: Use `Result<T>` for all command/query responses:
  ```csharp
  var result = await sender.Send(command);
  if (result.IsSuccess) return Ok(result.Value);
  return BadRequest(result.Error);
  ```

### Entity Design
- All entities inherit from `BaseEntity` with audit fields: `Id`, `CreatedBy`, `CreatedOn`, `ModifiedOn`, `ModifiedBy`
- Use `Guid` for all primary keys
- Navigation properties follow EF Core conventions

### Controller Patterns
- Use primary constructor injection: `public class Controller(ISender sender) : ControllerBase`
- Commands return `ActionResult<T>`, queries return `ActionResult<T>`
- Authentication required by default, use `[AllowAnonymous]` for public endpoints
- JWT tokens extracted from `Authorization: Bearer` header

### Validation & Error Handling
- **Pipeline Behavior**: `ValidationBehavior<TRequest, TResponse>` automatically validates all requests
- **Custom Exceptions**: `ValidationException`, `NotFoundException` handled by `GlobalExceptionHandler`
- **Problem Details**: Errors formatted as RFC 7231 problem details with extensions

### Repository Pattern
- **Unit of Work**: `IUnitOfWork` injected into handlers, provides access to all repositories
- **Generic Repository**: `IGenericRepository<T>` with `GetPagedAsync()` for pagination
- **Specific Repositories**: Feature-specific interfaces like `IProductRepository`

### Database & Migrations
- **PostgreSQL**: Connection string in `Api/appsettings.json`
- **EF Core**: Migrations in `infrastructure/Migrations/`
- **Auto-migration**: Database created/updated on app startup via `context.Database.MigrateAsync()`

## Development Workflow

### Getting Started
```bash
# Database setup (from infrastructure folder)
cd infrastructure
dotnet ef migrations add InitialCreate -s ../Api/Api.csproj
dotnet ef database update -s ../Api/Api.csproj

# Run API
cd Api
dotnet run
```

### Docker Development
```bash
# Start PostgreSQL
docker-compose up db -d

# Run with Docker
docker-compose up api
```

### Testing
- **API Testing**: Use `Api/Api.http` with REST Client extension - contains complete seeding workflow
- **Unit Tests**: `Tests/Inventory.UnitTests/`
- **Integration Tests**: `Tests/Inventory.IntegrationTests/`

## Common Implementation Patterns

### Adding New Feature
1. Create entities in `Core/Entities/`
2. Add DbSet to `AppDbContext`
3. Create repository interface in `Services/Abstractions/Persistence/`
4. Implement repository in `infrastructure/Repositories/`
5. Register repository in `InfrastructureRegistrations.cs`
6. Create commands/queries in `Services/Features/{Feature}/`
7. Add controller endpoints in `Api/Controllers/`

### Pagination
- Use `PagedRequest` as base for query requests
- Return `PagedResponse<T>` from handlers
- Use `ToPagedResponseAsync()` on `IQueryable<T>` or repository's `GetPagedAsync()`

### Authentication Flow
- **Signup/Login**: Returns `AuthResponse` with access/refresh tokens
- **Refresh**: Send refresh token to get new access token
- **Protected Routes**: Extract user ID from `User.FindFirstValue(ClaimTypes.NameIdentifier)`

### Configuration
- **JWT Settings**: `appsettings.json` under `Jwt` section
- **Database**: `ConnectionStrings.DefaultConnection`
- **Development**: Use `appsettings.Development.json` for overrides

## Code Quality Standards

### Naming Conventions
- **Projects**: PascalCase (Core, Services, Infrastructure)
- **Folders**: camelCase only for `infrastructure/` (historical artifact)
- **Classes**: PascalCase
- **Methods/Properties**: PascalCase
- **Private Fields**: camelCase with underscore prefix

### Dependencies
- **Core**: No external dependencies
- **Services**: MediatR, FluentValidation
- **Infrastructure**: EF Core, Identity
- **Api**: ASP.NET Core, Swagger

### File Organization
- One class per file
- Related files grouped in feature folders
- Configuration classes in `Config/` subfolders
- Extensions in `Extensions/` folders

## Key Files to Reference

- `Api/Program.cs`: Application startup and service registration
- `Core/Common/Result.cs`: Result pattern implementation
- `Services/Behaviors/ValidationBehavior.cs`: Request validation pipeline
- `Api/Middleware/GlobalExceptionHandler.cs`: Error handling
- `infrastructure/UnitOfWork.cs`: Transaction management
- `Api/Api.http`: Complete API testing workflow
- `docker-compose.yml`: Development environment setup</content>
<parameter name="filePath">/home/mutassem/MyProject/Inventory/Inventory/AGENTS.md
