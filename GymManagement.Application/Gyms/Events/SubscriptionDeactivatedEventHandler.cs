using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Events
{
    public class SubscriptionDeactivatedEventHandler : INotificationHandler<SubscriptionDeactivatedEvent>
    {
        private readonly IGymsRepository _gymsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SubscriptionDeactivatedEventHandler(IGymsRepository gymsRepository, IUnitOfWork unitOfWork)
        {
            _gymsRepository = gymsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(SubscriptionDeactivatedEvent notification, CancellationToken cancellationToken)
        {
            var gyms = await _gymsRepository.ListBySubscriptionIdAsync(notification.subscriptionId);

            if (gyms is null)
            {
                // Here we can send an emial or whatever error handling we want
                throw new InvalidOperationException($"The gyms related to the subscription with id '{notification.subscriptionId}' returnd null from the database");
            }
            gyms.ForEach(async gym =>
            {
                gym.Deactivate();
                await _gymsRepository.UpdateGymAsync(gym);
            });
            await _unitOfWork.CommitChnagesAsync();
        }
    }
}
