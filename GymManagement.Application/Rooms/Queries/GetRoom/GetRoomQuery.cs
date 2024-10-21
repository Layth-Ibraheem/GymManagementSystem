using ErrorOr;
using GymManagement.Application.Common.Authorization;
using GymManagement.Domain.Room;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Queries.GetRoom
{
    [Authorization(Domain.Admin.AdminRole.GetAllRooms)]
    public record GetRoomQuery(int roomId) : IRequest<ErrorOr<Room>>;
}
