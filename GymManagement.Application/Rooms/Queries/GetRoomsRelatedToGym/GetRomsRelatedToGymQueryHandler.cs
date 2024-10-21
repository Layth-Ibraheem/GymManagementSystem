using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Room;
using MediatR;

namespace GymManagement.Application.Rooms.Queries.GetRoomsRelatedToGym
{
    public class GetRomsRelatedToGymQueryHandler : IRequestHandler<GetRoomsRelatedToGymQuery, ErrorOr<List<Room>>>
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IGymsRepository _gymsRepository;


        public GetRomsRelatedToGymQueryHandler(IRoomsRepository roomsRepository, IGymsRepository gymsRepository)
        {
            _roomsRepository = roomsRepository;
            _gymsRepository = gymsRepository;
        }

        public async Task<ErrorOr<List<Room>>> Handle(GetRoomsRelatedToGymQuery request, CancellationToken cancellationToken)
        {
            var gym = await _gymsRepository.GetByIdAsync(request.gymId);
            if (gym is null)
            {
                return Error.NotFound(code: "Gym.NotFound", description: "There is no gym with such id");
            }
            var rooms = await _roomsRepository.ListByGymIdAsync(request.gymId);
            if (rooms is not null && rooms.Count > 0)
            {
                return rooms;
            }
            return Error.NotFound("Gym.HasNoRooms", "The gym doesn`t have any rooms yet");
        }
    }
}
