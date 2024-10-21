using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gym;
using GymManagement.Domain.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.CreateGym
{
    public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
    {
        private readonly IGymsRepository _gymsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        public CreateGymCommandHandler(IGymsRepository gymsRepository, IUnitOfWork unitOfWork, ISubscriptionsRepository subscriptionsRepository)
        {
            _gymsRepository = gymsRepository;
            _unitOfWork = unitOfWork;
            _subscriptionsRepository = subscriptionsRepository;
        }
        public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionsRepository.GetByIdAsync(request.subscriptionId);
            if (subscription is null)
            {
                return Error.NotFound("Subscription.NotFound", "There is no subscription with such id");
            }

            var gyms = await _gymsRepository.ListBySubscriptionIdAsync(subscription.Id);
            var gymsIds = gyms.Select(g => g.Id);
            subscription.LoadGymsIds(gymsIds);

            var gym = new Gym(request.name, subscription.Id, subscription.GetMaxRooms(), request.isActive);
            var assignGymToSubscriptionResult = gym.AssignToSubscription(subscription);
            if (assignGymToSubscriptionResult.IsError)
            {
                return assignGymToSubscriptionResult.FirstError;
            }
            await _gymsRepository.AddGymAsync(gym);
            await _unitOfWork.CommitChnagesAsync();
            return gym;
        }
    }
}
