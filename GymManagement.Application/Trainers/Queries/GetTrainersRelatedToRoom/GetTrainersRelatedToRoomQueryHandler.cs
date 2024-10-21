using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Trainer;
using MediatR;

namespace GymManagement.Application.Trainers.Queries.GetTrainersRelatedToRoom
{
    public class GetTrainersRelatedToRoomQueryHandler : IRequestHandler<GetTrainersRelatedToRoomQuery, ErrorOr<List<Trainer>>>
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly ITrainersRepository _trainerRepository;

        public GetTrainersRelatedToRoomQueryHandler(ITrainersRepository trainerRepository, IRoomsRepository roomsRepository)
        {
            _trainerRepository = trainerRepository;
            _roomsRepository = roomsRepository;
        }

        public async Task<ErrorOr<List<Trainer>>> Handle(GetTrainersRelatedToRoomQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomsRepository.GetByIdAsync(request.roomId);
            if (room is null)
            {
                return Error.NotFound(code: "Room.NotFound", description: "There is no room with sucn id");
            }

            var trainers = await _trainerRepository.ListByRoomIdAsync(room.Id);

            if (trainers is not null && trainers.Count > 0)
            {
                return trainers;
            }

            return Error.NotFound("Room.HasNoTrainers", "This room doesn`t have any trainers yet");
        }
    }
}
