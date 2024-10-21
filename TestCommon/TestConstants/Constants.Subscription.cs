using GymManagement.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommon.TestConstants
{
    public static partial class Constants
    {
        public static class Subscription
        {
            public static readonly int SubscriptionId = 1;
            public static readonly SubscriptionType SubscriptionType = SubscriptionType.Free;
            public static readonly bool IsActive = true;
        }
    }
}
