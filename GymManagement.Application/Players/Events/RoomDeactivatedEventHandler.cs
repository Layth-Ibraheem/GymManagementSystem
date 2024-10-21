using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Room.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Players.Events
{
    public class RoomDeactivatedEventHandler : INotificationHandler<RoomDeactivatedEvent>
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoomDeactivatedEventHandler(IPlayersRepository playersRepository, IUnitOfWork unitOfWork)
        {
            _playersRepository = playersRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(RoomDeactivatedEvent notification, CancellationToken cancellationToken)
        {
            var players = await _playersRepository.ListByRoomIdAsync(notification.roomId);
            if (players is null)
            {
                throw new InvalidOperationException($"The players related to the room with id '{notification.roomId}' returnd null from the database");

            }
            players.ForEach(async player =>
            {
                player.Deactivate();
                await _playersRepository.UpdatePlayerAsync(player);
            });

            await _unitOfWork.CommitChnagesAsync();
        }
    }
}
