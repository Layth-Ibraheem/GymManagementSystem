using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Trainers.Commands.CreateTrainer
{
    public class CreateTrainerCommandValidator : AbstractValidator<CreateTrainerCommand>
    {
        public CreateTrainerCommandValidator()
        {
            RuleFor(t => t.name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(t => t.height)
                .Must(h => h >= 100 && h <= 200)
                .WithMessage("The hieght must be between 100cm and 200cm");

            RuleFor(t => t.weight)
                .Must(w => w >= 30 && w <= 150)
                .WithMessage("The weight must be between 30kg and 150kg");
        }
    }
}
