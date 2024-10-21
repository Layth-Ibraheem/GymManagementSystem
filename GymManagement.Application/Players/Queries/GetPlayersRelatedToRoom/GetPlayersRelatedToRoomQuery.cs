using ErrorOr;
using GymManagement.Domain.Player;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Players.Queries.GetPlayersRelatedToRoom
{
    public record GetPlayersRelatedToRoomQuery(int roomId) : IRequest<ErrorOr<List<Player>>>;
}
