using CleanArchMediatR.Template.Application.Commands.Auth;
using FluentValidation;

namespace CleanArchMediatR.Template.Application.Validators.Auth
{
    public class RegisterUserValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterUserValidator() {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
