# Development Guide

This document provides a step-by-step guide on how to add new features to this template while maintaining the **Clean Architecture** and **MediatR** patterns.

## Workflow for Adding a New Feature (Example: GetUserById)

### 1. Domain Layer (The Core)
Define your entities or repository interfaces if they don't exist yet.
- **Path**: `src/CleanArchMediatR.Template.Domain/Interfaces/Repository/IUserRepository.cs`
- Ensure the domain remains independent of any external frameworks.

### 2. Application Layer (Business Logic)
This is where the MediatR magic happens. You need to create three parts:

#### A. Request (Query or Command)
Define the input model.
```csharp
public record GetUserQuery(Guid Id) : IRequest<UserDto>;
```

#### B. Validator (FluentValidation)
Create a validator to ensure the request is valid before hitting the handler.
```csharp
public class GetUserValidator : AbstractValidator<GetUserQuery>
{
    public GetUserValidator() => RuleFor(x => x.Id).NotEmpty();
}
```

#### C. Handler
Implement the logic. Inject your repositories and facades here.
```csharp
public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IUserRepository _repository;
    public GetUserHandler(IUserRepository repository) => _repository = repository;

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken ct) 
    {
        var user = await _repository.GetByIdAsync(request.Id);
        return new UserDto(user.Id, user.Username); // Consider using AutoMapper
    }
}
```

### 3. API Layer (The Entry Point)
Create or update a controller to expose the feature.
- **Rule**: Controllers should be "slim". They only send the request to MediatR.

```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetUser(Guid id)
{
    var result = await _mediator.Send(new GetUserQuery(id));
    return Ok(result);
}
```

## Best Practices
1.  **Always use DTOs**: Never return Domain Entities directly to the client.
2.  **Logic stays in Handlers**: Keep controllers clean. If logic is shared, move it to **Domain Services**.
3.  **Dependency Injection**: Use `RegisterServicesFromAssembly` in `Program.cs` to ensure new handlers are automatically discovered.
4.  **Logging**: Use Serilog for structured logging inside handlers to help with debugging.

---

## Testing Your Feature
After adding a feature, add a corresponding test in the `tests/` project:
1.  **Unit Test**: Mock the repository and test the Handler logic.
2.  **Integration Test**: Use `WebApplicationFactory` to test the full API flow.

```bash
# Run tests to verify
dotnet test
```