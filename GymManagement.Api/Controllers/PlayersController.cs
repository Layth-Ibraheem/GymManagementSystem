using GymManagement.Application.Players.Commands.CreatePlayer;
using GymManagement.Application.Players.Queries.GetPlayer;
using GymManagement.Contracts.Players;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("[controller]")]
    public class PlayersController : APIController
    {
        private readonly ISender _mediateR;

        public PlayersController(ISender mediateR)
        {
            _mediateR = mediateR;
        }

        [HttpPost("createPlayer")]
        public async Task<IActionResult> CreatePlayer(CreatePlayerRequest request)
        {
            var command = new CreatePlayerCommand(request.name, request.weight, request.height, request.isActive, request.roomId);

            var creatingPlayerResult = await _mediateR.Send(command);

            return creatingPlayerResult.Match(player =>
            {
                var response = new PlayerResponse(player.Id, player.Name, player.Weight, player.Height, player.IsActive, player.RoomId);
                return CreatedAtAction(nameof(GetPlayer), new { PlayerId = player.Id }, response);
            }, Problem);
        }

        [HttpGet("{playerId:int}")]
        public async Task<IActionResult> GetPlayer(int playerId)
        {
            var query = new GetPlayerQuery(playerId);

            var result = await _mediateR.Send(query);

            return result.Match(player =>
            {
                var response = new PlayerResponse(player.Id, player.Name, player.Weight, player.Height, player.IsActive, player.RoomId);
                return Ok(response);
            }, Problem);
        }

    }
}
