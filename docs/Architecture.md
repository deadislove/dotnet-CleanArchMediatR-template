# Architecture Overview

This project follows the **Clean Architecture** principle, aiming to separate concerns and make the system independent of UI, databases, or external frameworks.

## The Four Layers

### 1. Domain
The core of the application. It contains:
- **Entities**: Business objects.
- **Interfaces**: Repository abstractions.
- **Services**: Core business logic that doesn't fit in entities.

### 2. Application
Handles the orchestration of business rules. It uses **MediatR** to implement CQRS:
- **Commands/Queries**: Define the intent.
- **Handlers**: Execute the logic.
- **Validators**: FluentValidation rules.

### 3. Infrastructure
External concerns and implementations:
- **Data Access**: EF Core implementations.
- **Auth**: JWT token generation and validation.
- **Services**: External API clients or logging implementations.

### 4. API
The entry point of the application:
- **Controllers**: Slim wrappers that send requests to MediatR.
- **Middlewares**: Global exception handling and logging.

## Data Flow (Request / Response)
1. **Client** sends an HTTP request to the **API**.
2. **Controller** maps the request to a **MediatR Command/Query**.
3. **Validation Pipeline** checks the request.
4. **Handler** receives the command, interacts with **Domain Entities** and **Repositories**.
5. **Infrastructure** (Repository) persists changes to the database.
6. **Handler** returns a DTO to the **Controller**.
7. **API** returns the HTTP response.
