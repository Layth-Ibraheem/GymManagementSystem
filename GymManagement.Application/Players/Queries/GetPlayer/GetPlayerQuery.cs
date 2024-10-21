using ErrorOr;
using GymManagement.Domain.Player;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Players.Queries.GetPlayer
{
    public record GetPlayerQuery(int playerId) : IRequest<ErrorOr<Player>>;

}
