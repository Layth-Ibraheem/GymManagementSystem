using GymManagement.Application.Players.Queries.GetPlayersRelatedToRoom;
using GymManagement.Application.Rooms.Commands.CreateRoom;
using GymManagement.Application.Rooms.Queries.GetRoom;
using GymManagement.Application.Trainers.Queries.GetTrainersRelatedToRoom;
using GymManagement.Contracts.Players;
using GymManagement.Contracts.Rooms;
using GymManagement.Contracts.Trainers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainRoomType = GymManagement.Domain.Room.RoomType;
namespace GymManagement.Api.Controllers
{
    [Route("[controller]")]
    public class RoomsController : APIController
    {
        private readonly ISender _mediateR;
        public RoomsController(ISender mediateR)
        {
            _mediateR = mediateR;
        }

        [HttpPost("createNewRoom")]
        public async Task<IActionResult> CreateNewRoom(CreateRoomRequest request)
        {
            if (!DomainRoomType.TryFromName(request.roomType.ToString(), out DomainRoomType roomType))
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Invalid Subscription Type");
            }

            var command = new CreateRoomCommand(request.name, roomType, request.isActive, request.gymId);
            var cretingRoomResult = await _mediateR.Send(command);
            return cretingRoomResult.Match(
                room =>
                {
                    var roomResponse = new RoomResponse(room.Id,
                                                        Utils.FromDomainRoomTypeToPresintaionRoomType(room.RoomType),
                                                        room.Name,
                                                        room.IsActive,
                                                        room.GymId);
                    return CreatedAtAction(nameof(GetRoom), new { RoomId = room.Id }, roomResponse);
                },
                Problem
                );
        }

        [HttpGet("{roomId:int}")]
        public async Task<IActionResult> GetRoom(int roomId)
        {
            var query = new GetRoomQuery(roomId);
            var result = await _mediateR.Send(query);
            return result.Match(
                room =>
                {
                    var roomResponse = new RoomResponse(room.Id,
                                                        Utils.FromDomainRoomTypeToPresintaionRoomType(room.RoomType),
                                                        room.Name,
                                                        room.IsActive,
                                                        room.GymId);
                    return Ok(roomResponse);
                },
                Problem
                );
        }

        [HttpGet("{roomId:int}/players")]
        public async Task<IActionResult> GetPlayers(int roomId)
        {
            var query = new GetPlayersRelatedToRoomQuery(roomId);

            var result = await _mediateR.Send(query);

            return result.Match(players =>
            {
                var playerResponses = new List<PlayerResponse>();
                foreach (var player in players)
                {
                    playerResponses.Add(new PlayerResponse(player.Id, player.Name, player.Weight, player.Height, player.IsActive, player.RoomId));
                }
                var response = new GetPlayersRelatedToRoomResponse(roomId, playerResponses);
                return Ok(response);
            }, Problem);
        }

        [HttpGet("{roomId:int}/trainers")]
        public async Task<IActionResult> GetTrainers(int roomId)
        {
            var query = new GetTrainersRelatedToRoomQuery(roomId);

            var result = await _mediateR.Send(query);

            return result.Match(trainers =>
            {
                var trainersResponses = new List<TrainerResponse>();
                foreach (var trainer in trainers)
                {
                    trainersResponses.Add(new TrainerResponse(trainer.Id, trainer.Name, trainer.Weight, trainer.Height, trainer.IsActive, trainer.RoomId));
                }
                var response = new GetTrainersRelatedToRoomResponse(roomId, trainersResponses);
                return Ok(response);
            }, Problem);
        }
    }
}
