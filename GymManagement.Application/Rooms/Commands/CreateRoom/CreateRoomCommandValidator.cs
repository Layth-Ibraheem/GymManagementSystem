using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(r => r.name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20);
        }
    }
}
