using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Commands.DeactivateSubscription
{
    public class DeactivateSubscriptionCommandHandler : IRequestHandler<DeactivateSubscriptionCommand, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeactivateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Subscription>> Handle(DeactivateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionsRepository.GetByIdAsync(request.subscriptionId);
            if (subscription is null)
            {
                return Error.Conflict(code: "Subscription.NotFound",
                    description: "There is no subscription with such id");
            }
            subscription.Deactivate();
            await _subscriptionsRepository.UpdateSubscriptionAsync(subscription);
            await _unitOfWork.CommitChnagesAsync();
            return subscription;

        }
    }
}
