using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Player;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, ErrorOr<Player>>
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePlayerCommandHandler(IRoomsRepository roomsRepository, IPlayersRepository playersRepository, IUnitOfWork unitOfWork)
        {
            _roomsRepository = roomsRepository;
            _playersRepository = playersRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Player>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomsRepository.GetByIdAsync(request.roomId);
            if (room is null)
            {
                return Error.NotFound(code: "Room.NotFound", description: "There is no room with such id");
            }
            var players = await _playersRepository.ListByRoomIdAsync(room.Id);
            room.LoadPlayersIds(players.Select(p => p.Id));
            var player = new Player(request.name, request.height, request.weight, request.isActive, request.roomId);
            var addingPlayerToRoomResult = player.AssignToRoom(room);
            if (addingPlayerToRoomResult.IsError)
            {
                return addingPlayerToRoomResult.FirstError;
            }

            await _playersRepository.AddPlayerAsync(player);
            await _unitOfWork.CommitChnagesAsync();
            return player;
        }
    }
}
