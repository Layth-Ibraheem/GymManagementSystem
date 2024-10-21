using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Player;
using GymManagement.Domain.Room;
using GymManagement.Domain.Trainer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Queries.GetRoom
{
    public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, ErrorOr<Room>>
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IPlayersRepository _playersRepository;
        private readonly ITrainersRepository _trainersRepository;

        public GetRoomQueryHandler(IRoomsRepository roomsRepository, IPlayersRepository playersRepository, ITrainersRepository trainersRepository)
        {
            _roomsRepository = roomsRepository;
            _playersRepository = playersRepository;
            _trainersRepository = trainersRepository;
        }
        public async Task<ErrorOr<Room>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomsRepository.GetByIdAsync(request.roomId);
            if (room is null)
            {
                return Error.NotFound(code: "Room.NotFound", description: "There is no room with such id");
            }

            // we can retrieve only the Ids of the players | trainers but I retrive the actual objects in case I want to change the response


            var players = await _playersRepository.ListByRoomIdAsync(room.Id);
            var playersIds = players.Select(p => p.Id);

            var trainers = await _trainersRepository.ListByRoomIdAsync(room.Id);
            var trainerIds = trainers.Select(t => t.Id);

            room.LoadPlayersIds(playersIds);

            return room;

        }
    }
}
