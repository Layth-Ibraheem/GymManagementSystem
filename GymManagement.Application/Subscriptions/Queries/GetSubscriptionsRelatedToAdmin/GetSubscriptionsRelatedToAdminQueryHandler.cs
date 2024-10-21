using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Queries.GetSubscriptionsRelatedToAdmin
{
    public class GetSubscriptionsRelatedToAdminQueryHandler : IRequestHandler<GetSubscriptionsRelatedToAdminQuery, ErrorOr<List<Subscription>>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IAdminsRepository _adminsRepository;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetSubscriptionsRelatedToAdminQueryHandler(ISubscriptionsRepository subscriptionsRepository, IAdminsRepository adminsRepository, ICurrentUserProvider currentUserProvider)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _adminsRepository = adminsRepository;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<ErrorOr<List<Subscription>>> Handle(GetSubscriptionsRelatedToAdminQuery request, CancellationToken cancellationToken)
        {
           
            var admin = await _adminsRepository.GetByIdAsync(request.adminId);
            

            if (admin is null)
            {
                return Error.NotFound("Admin.NotFound", "There no admin with such id");
            }
            List<Subscription>? subscriptions = await _subscriptionsRepository.ListByAdminIdAsync(admin.Id);

            if (subscriptions is not null && subscriptions.Count > 0)
            {
                return subscriptions;
            }
            return Error.NotFound(code: "Admin.HasNoSubscriptions", description: "This admin doesn`t have any subscriptions yet");
        }
    }
}