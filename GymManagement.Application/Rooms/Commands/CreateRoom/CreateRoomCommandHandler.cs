using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gym;
using GymManagement.Domain.Room;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
    {
        private readonly IGymsRepository _gymsRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomCommandHandler(IRoomsRepository roomsRepository, IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork)
        {
            _roomsRepository = roomsRepository;
            _gymsRepository = gymsRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            Gym? gym = await _gymsRepository.GetByIdAsync(request.gymId);
            if (gym is null)
            {
                return Error.NotFound("Gym.NotFound", description: "There is no gym with such id");
            }
            var subscription = await _subscriptionsRepository.GetByIdAsync(gym.SubscriptionId);
            if (subscription is null)
            {
                return Error.NotFound("Subscription.NotFound", description: "There is no gym with such id");
            }
            var rooms = await _roomsRepository.ListByGymIdAsync(request.gymId);
            var roomsIds = rooms.Select(r => r.Id);
            gym.LoadRoomsIds(roomsIds);

            var room = new Room(request.name,
                                request.roomType,
                                subscription.GetMaxPlayersPerRoom(),
                                subscription.GetMaxTrainersPerRoom(),
                                request.isActive,
                                gym.Id);

            var assignRoomResult = room.AssignToGym(gym);
            if (assignRoomResult.IsError)
            {
                return assignRoomResult.FirstError;
            }
            await _roomsRepository.AddRoomAsync(room);
            await _unitOfWork.CommitChnagesAsync();
            return room;
        }
    }
}
