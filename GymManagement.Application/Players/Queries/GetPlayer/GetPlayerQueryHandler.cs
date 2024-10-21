using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Player;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Players.Queries.GetPlayer
{
    public class GetPlayerQueryHandler : IRequestHandler<GetPlayerQuery, ErrorOr<Player>>
    {
        private readonly IPlayersRepository _playersRepository;
        public GetPlayerQueryHandler(IPlayersRepository playersRepository)
        {
            _playersRepository = playersRepository;
        }
        public async Task<ErrorOr<Player>> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            var player = await _playersRepository.GetPlayerByIdAsync(request.playerId);
            if(player is null)
            {
                return Error.NotFound(code: "Player.NotFound", description: "There is no player with such id");
            }
            return player;
        }
    }
}
