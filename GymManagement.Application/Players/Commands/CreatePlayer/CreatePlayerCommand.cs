using ErrorOr;
using GymManagement.Domain.Player;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Players.Commands.CreatePlayer
{
    public record CreatePlayerCommand(string name, short weight, short height, bool isActive, int roomId) : IRequest<ErrorOr<Player>>;
}
