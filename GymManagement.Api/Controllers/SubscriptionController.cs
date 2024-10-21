using ErrorOr;
using GymManagement.Application.Gyms.Queries.GetGymsRelatedToSubscription;
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Subscriptions.Commands.DeactivateSubscription;
using GymManagement.Application.Subscriptions.Queries.GetSubscription;
using GymManagement.Contracts.Gyms;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;

namespace GymManagement.Api.Controllers
{
    [Route("[controller]")]
    
    public class SubscriptionController : APIController
    {
        private readonly ISender _mediator;
        public SubscriptionController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createSubscription")]
        public async Task<IActionResult> CreateNewSubscription(CreateSubscriptionRequest request)
        {
            if (!DomainSubscriptionType.TryFromName(request.Type.ToString(), out var subscriptionType))
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "Invalid Subscription Type");
            }
            var command = new CreateSubscriptionCommand(subscriptionType, request.AdminId, request.isActive);

            var createSubscriptionResult = await _mediator.Send(command);

            return createSubscriptionResult.Match(
                subscription =>
                {
                    return CreatedAtAction(
                                    nameof(GetSubscription),
                                    new { subscriptionId = subscription.Id },
                                    new SubscriptionResponse(subscription.Id,
                                                            Utils.FromDomainSubscriptionTypeToPresintationSubscriptionType(subscription.SubscriptionType),
                                                            subscription.IsActive, subscription.AdminId));
                },
                Problem);

        }
        [HttpGet("{subscriptionId:int}")]
        public async Task<IActionResult> GetSubscription(int subscriptionId)
        {
            var query = new GetSubscriptionQuery(subscriptionId);
            var getSubscriptionResult = await _mediator.Send(query);
            return getSubscriptionResult.Match(
                subscription =>
                {
                    return Ok(new SubscriptionResponse(subscription.Id,
                                                            Utils.FromDomainSubscriptionTypeToPresintationSubscriptionType(subscription.SubscriptionType),
                                                            subscription.IsActive,subscription.AdminId));
                },
                Problem);
        }

        [HttpPut("{subscriptionId:int}/deactivateSubscription")]
        public async Task<IActionResult> DeactivateSubscription(int subscriptionId)
        {
            var command = new DeactivateSubscriptionCommand(subscriptionId);

            var deactivationResult = await _mediator.Send(command);

            return deactivationResult.Match(
                subscription =>
                {
                    return Ok(new SubscriptionResponse(subscription.Id,
                                                            Utils.FromDomainSubscriptionTypeToPresintationSubscriptionType(subscription.SubscriptionType),
                                                            subscription.IsActive,subscription.AdminId));
                },
                Problem
                );
        }
        [HttpGet("{subscriptionId:int}/gyms")]
        public async Task<IActionResult> GetGyms(int subscriptionId)
        {
            var query = new GetGymsRelatedToSubscriptionQuery(subscriptionId);

            var result = await _mediator.Send(query);

            return result.Match(
                gyms =>
                {
                    var gymsResponses = new List<GymResponse>();
                    foreach (var gym in gyms)
                    {
                        gymsResponses.Add(new GymResponse(gym.Id, gym.Name, gym.SubscriptionId, gym.IsActive));
                    }
                    var getGymsRelatedToSubscriptionResponse = new GetGymsRelatedToSubscriptionResponse(subscriptionId, gymsResponses);
                    return Ok(getGymsRelatedToSubscriptionResponse);
                },
                Problem
                );
        }
    }
}
