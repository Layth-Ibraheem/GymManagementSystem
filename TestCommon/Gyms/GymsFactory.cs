using GymManagement.Domain.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.TestConstants;

namespace TestCommon.Gyms
{
    public static class GymsFactory
    {
        public static Gym CreateGym(
            int maxRooms,
            string? name = null,
            int? subscriptionId = null,
            bool? isActive = null,
            int? gymId = null)
        {
            return new Gym(name ?? Constants.Gym.Name,
                subscriptionId ?? Constants.Subscription.SubscriptionId,
                maxRooms,
                isActive ?? Constants.Gym.IsActive,
                gymId ?? Constants.Gym.GymId);
        }
    }
}
