# CleanArchMediatR.Template

## Description 

A modular .NET template project that implements Clean Architecture and MediatR, designed for scalable, testable, and maintainable enterprise applications. This solution also supports multiple database providers via factory abstraction and integrates JWT-based authentication with clear separation of concerns.

## 🚀 Features

- ✅ Clean Architecture (Application, Domain, Infra, API, Shared)
- ✅ CQRS with MediatR
- ✅ FluentValidation for request validation
- ✅ Serilog for structured logging
- ✅ JWT Authentication
- ✅ Modular DB support (MSSQL, PostgreSQL, SQLite via Docker Compose)
- ✅ Layered service abstraction (facade, repository, service)
- ✅ AutoMapper integration
- ✅ Docker-compatible
- ✅ xUnit Testing for Handlers
- ✅ Custom Middleware for exceptions and meta-filling

## 🧱 Folder Highlights

| Folder / Project                                    | Description                                                              |
| --------------------------------------------------- | ------------------------------------------------------------------------ |
| `src/CleanArchMediatR.Template.Api`                 | ASP.NET Core API project, entry point of the application                 |
| `src/CleanArchMediatR.Template.Application`         | Business logic with CQRS (Commands, Queries, Handlers, DTOs, Validators) |
| `src/CleanArchMediatR.Template.Domain`              | Domain layer with Entities, Interfaces, Services, and Exceptions         |
| `src/CleanArchMediatR.Template.Infra`               | Infrastructure logic (EF Core, Jwt, Logging, Repositories)               |
| `src/CleanArchMediatR.Template.PersistenceFactory`  | Database factory abstraction to support multiple DBs                     |
| `src/CleanArchMediatR.Template.Shared`              | Shared constants and utilities                                           |
| `tests/CleanArchMediatR.Template.Application.Tests` | xUnit tests for application layer                                        |
| `docker-compose.*.yml`                              | Scripts for launching various databases via Docker                       |

## 🧪 Running Tests

```bash
cd tests/CleanArchMediatR.Template.Application.Tests
dotnet test
```

## 🛠️ How to Run

```bash
# Launch database with docker (choose one)
docker-compose -f docker-compose.sqlite.yml up -d

# Build and run API
cd src/CleanArchMediatR.Template.Api
dotnet run
```

## 📦 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker](https://www.docker.com/) (if using provided database images)

## 🧠 Architecture Overview

The solution follows Clean Architecture principles:

```bash
API (Controller)
  └── Application Layer (Commands, Queries, Handlers)
       └── Domain Layer (Entities, Interfaces)
            └── Infrastructure (EFCore, Jwt, Logging)
```

Decoupling is achieved via abstractions and dependency injection. The PersistenceFactory adds flexibility for database provider configuration.

## ⚙️ Customize the Template

To start your own project based on this template:

```bash
dotnet new install CleanArchMediatR.Template
dotnet new cleanarch-mediatr -n YourProjectName
```

## 📌 Other Notes

- SQLite DB (app.db) is used for lightweight development.
- Logs are saved in logs/ directory.
- HTTP samples can be tested with *.http files in API project.

## Stay in touch

- Author - [Da-Wei Lin](https://www.linkedin.com/in/da-wei-lin-689a35107/)
- Website - [David Weblog](https://davidskyspace.com/)
- [MIT LICENSE](https://github.com/deadislove/dotnet-CleanArchMediatR-template/blob/main/LICENSE.md)
