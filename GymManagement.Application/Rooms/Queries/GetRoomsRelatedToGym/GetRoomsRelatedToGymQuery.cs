using ErrorOr;
using GymManagement.Domain.Room;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Queries.GetRoomsRelatedToGym
{
    public record GetRoomsRelatedToGymQuery(int gymId) : IRequest<ErrorOr<List<Room>>>;
}
