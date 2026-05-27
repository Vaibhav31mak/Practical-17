# Practical17 - .NET 10 Web API

A clean and scalable ASP.NET Core Web API built with **.NET 10**, following **Clean Architecture principles**, **Repository + Unit of Work**, **Result Pattern**, **JWT Authentication**, and **Role-Based Authorization**.

Supports:

* Student Management APIs
* JWT Authentication with Identity
* Admin/User roles
* Auditing with EF Core Interceptors
* Global Exception Handling
* FluentValidation
* AutoMapper
* Swagger/OpenAPI

---

# Project Structure

```txt
Practical17.Domain
│
├── Common
│   ├── AuditingContracts
│   │   ├── IEntity
│   │   ├── ICreatable
│   │   ├── IUpdatable
│   │   ├── ISoftDeletable
│   │   └── IConcurrencyCheck
│   │
│   ├── AuditingEntities
│   │   └── BaseEntity
│   │
│   └── ResultPattern
│       └── Result
│
└── Entities
    └── Student


Practical17.Application
│
├── Contracts
│   ├── IStudentService
│   └── IAuthService
│
├── Dtos
│   ├── Students
│   └── Auth
│
├── Mapping
│   └── StudentProfile
│
├── Services
│   ├── StudentService
│   └── AuthService
│
├── Validation
│   └── Students
│
└── DependencyInjection
    └── ApplicationServicesRegistration


Practical17.Infrastructure
│
├── Data
│   ├── DbContext
│   │   └── StudentDbContext
│   │
│   └── Interceptors
│       └── AuditingSaveChangesInterceptor
│
├── Identity
│   ├── ApplicationUser
│   └── IdentitySeeder
│
├── Repositories
│   ├── Contracts
│   └── Implementations
│
├── UnitOfWorkPattern
│   ├── IUnitOfWork
│   └── UnitOfWork
│
└── DependencyInjection
    └── InfrastructureServiceRegistration


Practical17.Api
│
├── Endpoints
│   ├── StudentEndpoints
│   └── AuthEndpoints
│
├── GlobalExceptionHandler
├── GlobalUsings
├── Program.cs
└── appsettings.json
```

---

# Features

* ASP.NET Core Minimal APIs
* Entity Framework Core
* ASP.NET Core Identity
* JWT Authentication
* Role-Based Authorization
* Repository Pattern
* Unit of Work Pattern
* Result Pattern
* AutoMapper
* FluentValidation
* EF Core SaveChanges Interceptor
* Swagger/OpenAPI
* Global Exception Handling
* Soft Delete + Auditing

---

# Authentication & Authorization

The API uses:

* JWT Bearer Authentication
* ASP.NET Core Identity
* Role-Based Authorization

## Roles

| Role  | Permissions            |
| ----- | ---------------------- |
| Admin | Create, Update, Delete |
| User  | Read Only              |

---

# JWT Configuration

Configure JWT inside `appsettings.json`.

```json
{
  "Jwt": {
    "Issuer": "Practical17",
    "Audience": "Practical17Users",
    "Key": "ChangeThisToASecureKeyWithAtLeast32Chars",
    "ExpiryMinutes": 60
  }
}
```

---

# Default Seeded Admin

`IdentitySeeder` automatically creates:

## Roles

* Admin
* User

## Default Admin User

```txt
Email    : admin@gmail.com
Password : Admin@123
Role     : Admin
```

---

# Technologies Used

| Technology       | Purpose           |
| ---------------- | ----------------- |
| .NET 10          | Backend Framework |
| EF Core          | ORM               |
| SQL Server       | Database          |
| ASP.NET Identity | User Management   |
| JWT              | Authentication    |
| AutoMapper       | DTO Mapping       |
| FluentValidation | Validation        |
| Swagger          | API Documentation |

---

Add this section near the end of the README:

---

# Code Quality & Documentation

The project follows clean and maintainable coding practices with strong focus on readability and scalability.

## Included Practices

* XML documentation summaries for methods and services
* Meaningful inline comments for complex business logic
* Clear naming conventions
* Interface-driven architecture
* Separation of concerns across layers
* Reusable service and repository abstractions
* Centralized validation and exception handling

## Documentation Style

The codebase includes:

* Method summaries explaining purpose and behavior
* Parameter and return value documentation
* Comments for important architectural decisions
* Readable and self-explanatory business logic

Example:

```csharp
/// <summary>
/// Creates a new student and stores it in the database.
/// </summary>
/// <param name="dto">Student creation request.</param>
/// <returns>Created student details.</returns>
public async Task<Result<StudentDto>> CreateAsync(CreateStudentDto dto)
{
    // Map DTO to entity
    var student = mapper.Map<Student>(dto);

    await repository.AddAsync(student);

    // Persist changes using Unit Of Work
    await unitOfWork.SaveChangesAsync();

    return Result<StudentDto>.Success(
        mapper.Map<StudentDto>(student));
}
```

Benefits:

* Easier onboarding for developers
* Better maintainability
* Improved debugging experience
* Cleaner API understanding
* Better IDE IntelliSense support

---


# Setup Instructions

## 1. Clone Repository

```bash
git clone <repository-url>
cd Practical17
```

---

## 2. Configure Database

Update connection string inside:

```txt
Practical17.Api/appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=Practical17Db;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

---

## 3. Apply Migrations

```bash
dotnet ef database update
```

---

## 4. Run Project

```bash
dotnet run --project Practical17.Api
```

---

# Swagger

Swagger is enabled by default.

Available at:

```txt
https://localhost:<port>/swagger
```

Supports:

* JWT Authentication
* Authorized endpoint testing

---

# Authentication Flow

## Register

User registers using:

```http
POST /api/auth/register
```

---

## Login

User logs in using:

```http
POST /api/auth/login
```

Returns:

```json
{
  "token": "JWT_TOKEN",
  "email": "user@example.com",
  "roles": ["User"]
}
```

---

## Use Token

Add JWT token in Swagger or request header:

```http
Authorization: Bearer YOUR_TOKEN
```

---

# Validation

Validation is implemented using **FluentValidation**.

No Data Annotations are used.

Examples:

* `CreateStudentValidator`
* `UpdateStudentValidator`

Benefits:

* Clean separation of concerns
* Reusable validation logic
* Better maintainability

---

# Auditing

Auditing is handled using:

```txt
AuditingSaveChangesInterceptor
```

Automatically manages:

* CreatedBy
* CreatedOn
* UpdatedBy
* UpdatedOn
* DeletedBy
* DeletedOn
* IsDeleted

Applied during `SaveChangesAsync()`.

---

# Result Pattern

All services return a standardized `Result<T>` object.

Benefits:

* Consistent API responses
* Cleaner error handling
* Avoids exception-driven business logic

Example:

```csharp
return Result<StudentDto>.Success(studentDto);

return Result<StudentDto>.Failure("Student not found.");
```

---

# Repository + Unit of Work

## Repository

Encapsulates data access logic.

Example:

```csharp
IRepository<Student>
```

## Unit of Work

Coordinates transactional operations.

Example:

```csharp
await unitOfWork.SaveChangesAsync();
```

Benefits:

* Cleaner architecture
* Better testability
* Centralized transaction handling

---

# AutoMapper

Used for DTO ↔ Entity mapping.

Example:

```csharp
CreateMap<Student, StudentDto>();
```

Profile:

```txt
StudentProfile
```

---

# Global Exception Handling

Unhandled exceptions are processed centrally using:

```txt
GlobalExceptionHandler
```

Provides:

* Consistent error responses
* Cleaner endpoints
* Centralized logging support

---

# API Endpoints

# Authentication

| Method | Endpoint             | Access |
| ------ | -------------------- | ------ |
| POST   | `/api/auth/register` | Public |
| POST   | `/api/auth/login`    | Public |

---

# Students

| Method | Endpoint             | Access     |
| ------ | -------------------- | ---------- |
| GET    | `/api/students`      | User/Admin |
| GET    | `/api/students/{id}` | User/Admin |
| POST   | `/api/students`      | Admin      |
| PUT    | `/api/students/{id}` | Admin      |
| DELETE | `/api/students/{id}` | Admin      |

---

# Architecture Highlights

* Clean separation of layers
* Dependency Injection throughout application
* Minimal API endpoints
* Interface-driven development
* Extensible and scalable structure
* Centralized validations and exception handling

---

# Future Improvements

* Refresh Tokens
* Rate Limiting
* API Versioning
* Serilog Logging
* CQRS + MediatR
* Redis Caching
* Docker Support
* Health Checks
* OpenTelemetry

---
