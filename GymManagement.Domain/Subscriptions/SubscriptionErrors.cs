using ErrorOr;

namespace GymManagement.Domain.Subscriptions;

public static class SubscriptionErrors
{
    public static readonly Error CannotHaveMoreGymsThanTheSubscriptionAllows = Error.Validation(
        code: "Subscription.CannotHaveMoreGymsThanTheSubscriptionAllows",
        description: "A subscription cannot have more gyms than the subscription allows");

    public static readonly Error InActiveSubscription = Error.Validation(
        code: "Subscription.InActiveSubscription",
        description: "This subscription is not active");
}