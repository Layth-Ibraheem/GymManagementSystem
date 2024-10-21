using ErrorOr;
using MediatR;
using GymManagement.Domain.Subscriptions;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin;
namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminsRepository _adminsRepository;

        public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork, IAdminsRepository adminsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _unitOfWork = unitOfWork;
            _adminsRepository = adminsRepository;
        }

        public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            Admin? admin = await _adminsRepository.GetByIdAsync(request.AdminId);

            if (admin is null)
            {
                return Error.NotFound(code: "Admin.NotFound", description: "Admin not found");
            }
            
            List<Subscription>? adminSubscriptions = await _subscriptionsRepository.ListByAdminIdAsync(admin.Id);
            if(adminSubscriptions is not null && adminSubscriptions.Count > 0)
            {
                var result = admin.CanHasASubscription(adminSubscriptions);
                if (result.IsError)
                {
                    return result.FirstError;
                }
            }


            var subscription = new Subscription(request.SubscriptionType, admin.Id, request.IsActive);

            await _subscriptionsRepository.AddSubscriptionAsync(subscription);
            await _unitOfWork.CommitChnagesAsync();
            return subscription;
        }

    }
}
