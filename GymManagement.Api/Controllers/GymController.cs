using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Queries.GetGym;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("[controller]")]
    public class GymController : APIController
    {
        private readonly ISender _mediatR;
        public GymController(ISender mediatR)
        {
            _mediatR = mediatR;
        }
        [HttpPost("addGym")]
        public async Task<IActionResult> AddGym(CreateGymRequest request)
        {
            var command = new CreateGymCommand(request.name, request.subscriptionId, true);

            var addingGymResult = await _mediatR.Send(command);

            return addingGymResult.Match(
                gym => CreatedAtAction(
                    nameof(GetGym),
                    new { gymId = gym.Id },
                    new GymResponse(gym.Id, gym.Name, gym.SubscriptionId, gym.IsActive)),
                Problem);
        }

        [HttpGet("{gymId:int}")]
        public async Task<IActionResult> GetGym(int gymId)
        {
            var query = new GetGymQuery(gymId);

            var result = await _mediatR.Send(query);

            return result.Match(
                gym => Ok(new GymResponse(gym.Id, gym.Name, gym.SubscriptionId, gym.IsActive)),
                Problem);
        }
    }
}
