using FluentValidation;
using GymManagement.Application.Common.Utils;

namespace GymManagement.Application.Admins.Commands.CreateAdmin
{
    public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommand>
    {
        public CreateAdminCommandValidator()
        {
            RuleFor(a => a.name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20);

            RuleFor(a => a.userName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20);

            RuleFor(a => a.password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(25)
                .Must(ContainsFunctions.ContainLetters).WithMessage("Password must contain at least two letters.")
                .Must(ContainsFunctions.ContainDigits).WithMessage("Password must contain at least two digits.")
                .Must(ContainsFunctions.ContainSpecialChars).WithMessage("Password must contain at least two special characters.");

        }
        
    }
}
