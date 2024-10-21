using ErrorOr;
using FluentAssertions;
using GymManagement.Domain.Gym;
using GymManagement.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Gyms;
using TestCommon.Subscriptions;

namespace GymManagement.Domain.UnitTests.Subscriptions
{

    public class SubscriptionsTests
    {
        [Fact]
        public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
        {
            // Arrange: 
            // 1- Create new subscription
            var subscription = SubscriptionsFactory.CreateSubscription(subscriptionId: 1);

            // 2- Create the maximum number of gyms + 1
            int maxRooms = subscription.GetMaxRooms();
            int maxGyms = subscription.GetMaxGyms();
            var gyms = new List<Gym.Gym>();
            for (int i = 1; i <= maxGyms + 1; i++)
            {
                gyms.Add(GymsFactory.CreateGym(maxRooms, gymId: i));
            }

            // Act: 
            // 1- Add all the gyms
            var addGymResults = gyms.ConvertAll(gym => subscription.AddGym(gym.Id));

            // Assert
            // 1- The operation will success to all gyms but the last gym shoud fails.
            var allButLastGymResults = addGymResults[..^1];
            allButLastGymResults.Should().AllSatisfy(addGymResult => addGymResult.Value.Should().Be(Result.Success));

            var lastAddGymResult = addGymResults.Last();
            lastAddGymResult.IsError.Should().BeTrue();
            lastAddGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows);
        }

        [Fact]
        public void AddGym_WhenSubscriptionIsNotActive_ShouldFail()
        {
            // Arrange
            // 1- Create new subscription and set the is active to false
            var subscription = SubscriptionsFactory.CreateSubscription(isActive: false);
            // 2- Create new gym
            int maxRooms = subscription.GetMaxRooms();
            var gym = GymsFactory.CreateGym(maxRooms);

            // Act
            // 1- Add the gym to the subscription
            var result = subscription.AddGym(gym.Id);

            // Assert
            // 1- The operation will fail and should return the failure reason
            result.IsError.Should().BeTrue();
            result.FirstError.Should().Be(SubscriptionErrors.InActiveSubscription);
        }

        [Fact]
        public void AddGym_WhenSubscriptionIsActiveAndWithinTheAllowedGyms_ShouldSuccess()
        {
            // Arrange
            // 1- Create new subscription
            var subscription = SubscriptionsFactory.CreateSubscription();
            // 2- Create new gym
            int maxRooms = subscription.GetMaxRooms();
            var gym = GymsFactory.CreateGym(maxRooms);

            // Act
            // 1- Add the gym to the subscription
            var result = subscription.AddGym(gym.Id);

            // Assert
            // 1- The operation will success
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(Result.Success);
        }
    }
}
