using ErrorOr;
using GymManagement.Domain.Trainer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Trainers.Queries.GetTrainersRelatedToRoom
{
    public record GetTrainersRelatedToRoomQuery(int roomId) : IRequest<ErrorOr<List<Trainer>>>;
}
