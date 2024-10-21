using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Room.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Trainers.Events
{
    public class RoomDeactivatedEventHandler : INotificationHandler<RoomDeactivatedEvent>
    {
        private readonly ITrainersRepository _trainersRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoomDeactivatedEventHandler(ITrainersRepository trainersRepository, IUnitOfWork unitOfWork)
        {
            _trainersRepository = trainersRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(RoomDeactivatedEvent notification, CancellationToken cancellationToken)
        {
            var trainers = await _trainersRepository.ListByRoomIdAsync(notification.roomId);
            if (trainers is null)
            {
                throw new InvalidOperationException($"The trainers related to the room with id '{notification.roomId}' returnd null from the database");

            }

            trainers.ForEach(async trainer =>
            {
                trainer.Deactivate();
                await _trainersRepository.UpdateTrainerAsync(trainer);
            });

            await _unitOfWork.CommitChnagesAsync();
        }
    }
}
