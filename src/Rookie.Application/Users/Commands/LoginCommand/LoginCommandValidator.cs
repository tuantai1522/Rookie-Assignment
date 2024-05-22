using FluentValidation;

namespace Rookie.Application.Users.Commands.LoginCommand
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}