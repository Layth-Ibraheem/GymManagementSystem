using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Commands.DeactivateSubscription
{
    public record DeactivateSubscriptionCommand(int subscriptionId) : IRequest<ErrorOr<Subscription>>;
}
