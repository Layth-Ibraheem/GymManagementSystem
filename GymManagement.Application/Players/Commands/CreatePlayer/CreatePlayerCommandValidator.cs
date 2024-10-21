using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        public CreatePlayerCommandValidator()
        {
            // We can define a settings table in the database and extract these information from it instead of hardcoding them
            RuleFor(p => p.name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(p => p.height)
                .Must(h => h >= 100 && h <= 200)
                .WithMessage("The hieght must be between 100cm and 200cm");

            RuleFor(p => p.weight)
                .Must(h => h >= 30 && h <= 150)
                .WithMessage("The weight must be between 30kg and 150kg");
        }
    }
}
