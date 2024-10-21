using GymManagement.Application.Trainers.Commands.CreateTrainer;
using GymManagement.Application.Trainers.Queries.GetTrainer;
using GymManagement.Contracts.Trainers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("[controller]")]
    public class TrainersController : APIController
    {
        private readonly ISender _mediateR;
        public TrainersController(ISender mediateR)
        {
            _mediateR = mediateR;
        }

        [HttpPost("createNewTrainer")]
        public async Task<IActionResult> CreateNewTrainer(CreateTrainerRequest request)
        {
            var command = new CreateTrainerCommand(request.name, request.weight, request.height, request.isActive, request.roomId);

            var creatingTrainerResult = await _mediateR.Send(command);

            return creatingTrainerResult.Match(
                trainer =>
                {
                    var response = new TrainerResponse(trainer.Id, trainer.Name, trainer.Weight, trainer.Height, trainer.IsActive, trainer.RoomId);
                    return CreatedAtAction(nameof(GetTrainer), new { TrainerId = trainer.Id }, response);
                },
                Problem
                );
        }
        [HttpGet("{trainerId:int}")]
        public async Task<IActionResult> GetTrainer(int trainerId)
        {
            var query = new GetTrainerQuery(trainerId);

            var result = await _mediateR.Send(query);

            return result.Match(
                trainer =>
                {
                    var response = new TrainerResponse(trainer.Id, trainer.Name, trainer.Weight, trainer.Height, trainer.IsActive, trainer.RoomId);
                    return Ok(response);
                },
                Problem
                );
        }
    }
}
