using ErrorOr;
using GymManagement.Domain.Gym;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Queries.GetGymsRelatedToSubscription
{
    public record GetGymsRelatedToSubscriptionQuery(int subscriptionId) : IRequest<ErrorOr<List<Gym>>>;
}
