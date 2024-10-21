using ErrorOr;
using GymManagement.Application.Common.Authorization;
using GymManagement.Domain.Admin;
using GymManagement.Domain.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Queries.GetSubscriptionsRelatedToAdmin
{
    [Authorization(AdminRole.GetAllSubscriptions)]
    public record GetSubscriptionsRelatedToAdminQuery(int adminId) : IRequest<ErrorOr<List<Subscription>>>;
}
