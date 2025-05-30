using CleanArchMediatR.Template.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace CleanArchMediatR.Template.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
                ValidationResult[] validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                List<ValidationFailure> failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Any())
                {
                    Dictionary<string, string[]> errors = failures
                        .GroupBy(f => f.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => x.ErrorMessage).ToArray()
                        );
                    string errorsString = string.Join("; ",errors.SelectMany(kvp => kvp.Value.Select(msg => $"{kvp.Key}: {msg}")));

                    throw new DomainException("Validation failed." + errorsString, (int)HttpStatusCode.BadRequest, errors);
                }
            }

            return await next();
        }
    }
}
