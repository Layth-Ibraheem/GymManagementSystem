using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Trainer;
using MediatR;

namespace GymManagement.Application.Trainers.Commands.CreateTrainer
{
    public class CreateTrainerCommandHandler : IRequestHandler<CreateTrainerCommand, ErrorOr<Trainer>>
    {
        private readonly ITrainersRepository _trainersRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateTrainerCommandHandler(ITrainersRepository trainersRepository, IRoomsRepository roomsRepository, IUnitOfWork unitOfWork)
        {
            _trainersRepository = trainersRepository;
            _roomsRepository = roomsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Trainer>> Handle(CreateTrainerCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomsRepository.GetByIdAsync(request.roomId);
            if (room is null)
            {
                return Error.NotFound(code: "Room.NotFound", description: "There is no room with sucn id");
            }
            var trainers = await _trainersRepository.ListByRoomIdAsync(room.Id);
            room.LoadTrainerIds(trainers.Select(t => t.Id));
            Trainer trainer = new Trainer(request.name, request.isActive, request.height, request.weight, request.roomId);

            var addingTrainerResult = trainer.AssignToRoom(room);
            if (addingTrainerResult.IsError)
            {
                return addingTrainerResult.FirstError;
            }

            await _trainersRepository.AddTrainerAsync(trainer);
            await _unitOfWork.CommitChnagesAsync();

            return trainer;

        }
    }
}
