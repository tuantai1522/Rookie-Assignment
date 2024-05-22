using FluentValidation;
using Rookie.Application.Users.ViewModels;

namespace Rookie.Application.Users.Commands.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}