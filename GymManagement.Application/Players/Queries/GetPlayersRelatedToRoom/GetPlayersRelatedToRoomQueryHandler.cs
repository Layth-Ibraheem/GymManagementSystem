using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Player;
using MediatR;

namespace GymManagement.Application.Players.Queries.GetPlayersRelatedToRoom
{
    public class GetPlayersRelatedToRoomQueryHandler : IRequestHandler<GetPlayersRelatedToRoomQuery, ErrorOr<List<Player>>>
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IRoomsRepository _roomsRepository;

        public GetPlayersRelatedToRoomQueryHandler(IRoomsRepository roomsRepository, IPlayersRepository playersRepository)
        {
            _roomsRepository = roomsRepository;
            _playersRepository = playersRepository;
        }

        public async Task<ErrorOr<List<Player>>> Handle(GetPlayersRelatedToRoomQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomsRepository.GetByIdAsync(request.roomId);
            if (room is null)
            {
                return Error.NotFound(code: "Room.NotFound", description: "There is no room with sucn id");
            }

            var players = await _playersRepository.ListByRoomIdAsync(room.Id);
            if (players is not null && players.Count > 0)
            {
                return players;
            }
            return Error.NotFound("Room.HasNoPlayers", "This room doesn`t have any players yet");
        }
    }
}
