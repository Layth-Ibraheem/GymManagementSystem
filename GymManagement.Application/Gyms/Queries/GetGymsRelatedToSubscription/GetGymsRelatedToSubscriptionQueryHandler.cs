using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gym;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.GetGymsRelatedToSubscription
{
    public class GetGymsRelatedToSubscriptionQueryHandler : IRequestHandler<GetGymsRelatedToSubscriptionQuery, ErrorOr<List<Gym>>>
    {
        private readonly IGymsRepository _gymsRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public GetGymsRelatedToSubscriptionQueryHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository)
        {
            _gymsRepository = gymsRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<ErrorOr<List<Gym>>> Handle(GetGymsRelatedToSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionsRepository.GetByIdAsync(request.subscriptionId);
            if (subscription is null)
            {
                return Error.NotFound("Subscription.NotFound", description: "There is no subscription with such id");
            }
            var gyms = await _gymsRepository.ListBySubscriptionIdAsync(subscription.Id);
            if (gyms is not null && gyms.Count > 0)
            {
                return gyms;
            }
            return Error.NotFound("Subscription.HasNoGyms", "This subscription doesn`t have any gyms yet");
        }
    }
}
