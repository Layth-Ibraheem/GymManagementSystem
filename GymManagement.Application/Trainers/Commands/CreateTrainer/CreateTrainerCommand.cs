using ErrorOr;
using GymManagement.Domain.Trainer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Trainers.Commands.CreateTrainer
{
    public record CreateTrainerCommand(string name, short weight, short height, bool isActive, int roomId) : IRequest<ErrorOr<Trainer>>;
}
