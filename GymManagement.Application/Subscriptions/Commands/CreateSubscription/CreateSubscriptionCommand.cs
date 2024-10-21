using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription
{
    public record CreateSubscriptionCommand(SubscriptionType SubscriptionType, int AdminId, bool IsActive): IRequest<ErrorOr<Subscription>>;
    
}
