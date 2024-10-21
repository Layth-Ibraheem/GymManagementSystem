using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin.Events;
using MediatR;

namespace GymManagement.Application.Subscriptions.Events
{
    public class AdminDeactivatedEventHandler : INotificationHandler<AdminDeactivatedEvent>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AdminDeactivatedEventHandler(ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(AdminDeactivatedEvent notification, CancellationToken cancellationToken)
        {
            var subscriptions = await _subscriptionsRepository.ListByAdminIdAsync(notification.adminId);
            if (subscriptions is null)
            {
                throw new InvalidOperationException($"The subscriptions related to the admini with id '{notification.adminId}' returnd null from the database");

            }
            subscriptions.ForEach(async subscription =>
            {
                subscription.Deactivate();
                await _subscriptionsRepository.UpdateSubscriptionAsync(subscription);
            });

            await _unitOfWork.CommitChnagesAsync();
        }
    }
}
