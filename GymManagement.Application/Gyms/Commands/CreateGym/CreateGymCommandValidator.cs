using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.CreateGym
{
    public class CreateGymCommandValidator : AbstractValidator<CreateGymCommand>
    {
        public CreateGymCommandValidator()
        {
            RuleFor(g => g.name)
                .MinimumLength(5)
                .MaximumLength(50);
        }
    }
}
