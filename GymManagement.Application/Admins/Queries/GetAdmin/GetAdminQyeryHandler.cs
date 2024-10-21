using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin;
using MediatR;

namespace GymManagement.Application.Admins.Queries.GetAdmin
{
    public class GetAdminQyeryHandler : IRequestHandler<GetAdminQuery, ErrorOr<Admin>>
    {
        private readonly IAdminsRepository _adminsRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        public GetAdminQyeryHandler(IAdminsRepository adminsRepository, ISubscriptionsRepository subscriptionsRepository)
        {
            _adminsRepository = adminsRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<ErrorOr<Admin>> Handle(GetAdminQuery request, CancellationToken cancellationToken)
        {
            var admin = await _adminsRepository.GetByIdAsync(request.adminId);
            if (admin is null)
            {
                return Error.NotFound(code: "Admin.NotFound", description: "There is no admin with such id");
            }

            return admin;
        }
    }
}
