using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gym.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Events
{
    public class GymDeactivatedEventHandler : INotificationHandler<GymDeactivatedEvent>
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GymDeactivatedEventHandler(IRoomsRepository roomsRepository, IUnitOfWork unitOfWork)
        {
            _roomsRepository = roomsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(GymDeactivatedEvent notification, CancellationToken cancellationToken)
        {
            var rooms = await _roomsRepository.ListByGymIdAsync(notification.gymId);
            if (rooms is null)
            {
                throw new InvalidOperationException($"The rooms related to the gym with id '{notification.gymId}' returnd null from the database");

            }
            rooms.ForEach(async room =>
            {
                room.Deactivate();
                await _roomsRepository.UpdateRoom(room);
            });

            await _unitOfWork.CommitChnagesAsync();
        }
    }
}
