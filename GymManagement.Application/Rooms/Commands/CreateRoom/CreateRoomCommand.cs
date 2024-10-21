using ErrorOr;
using GymManagement.Domain.Room;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(string name, RoomType roomType,bool isActive, int gymId) : IRequest<ErrorOr<Room>>;
}
