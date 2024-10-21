using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IGymsRepository _gymsRepository;

        public GetSubscriptionQueryHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _gymsRepository = gymsRepository;
        }

        public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);
            if (subscription is null)
            {
                return Error.NotFound(description: "Subscription Is Not Found");
            }
            var gyms = await _gymsRepository.ListBySubscriptionIdAsync(subscription.Id);
            if(gyms.Count == 0)
            {
                return subscription;

            }
            var gymsIds = gyms.Select(g => g.Id).ToList();

            subscription.LoadGymsIds(gymsIds);

            return subscription;
        }
    }
}
