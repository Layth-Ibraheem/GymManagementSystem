using GymManagement.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.TestConstants;

namespace TestCommon.Subscriptions
{
    public static class SubscriptionsFactory
    {
        public static Subscription CreateSubscription(
            int? subscriptionId = null,
            int? adminId = null,
            SubscriptionType? subscriptionType = null,
            bool? isActive = null)
        {
            return new Subscription(
                subscriptionType: subscriptionType ?? Constants.Subscription.SubscriptionType,
                adminId: adminId ?? Constants.Admins.AdminId,
                isActive: isActive ?? Constants.Subscription.IsActive,
                subscriptionId: subscriptionId ?? Constants.Subscription.SubscriptionId);
        }
    }
}
