using ErrorOr;
using GymManagement.Domain.Trainer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Trainers.Queries.GetTrainer
{
    public record GetTrainerQuery(int trainerId) : IRequest<ErrorOr<Trainer>>;
}
