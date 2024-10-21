using ErrorOr;
using FluentAssertions;
using GymManagement.Domain.Gym;
using GymManagement.Domain.Room;
using GymManagement.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Gyms;
using TestCommon.Rooms;
using TestCommon.Subscriptions;

namespace GymManagement.Domain.UnitTests.Gyms
{
    public class GymsTests
    {
        [Fact]
        public void AddRoom_WhenMoreThanSubscriptionAllows_ShouldFail()
        {
            // Arrange
            // 1- Create new subscription and set it`s type to a specific type
            var subscription = SubscriptionsFactory.CreateSubscription(subscriptionType: SubscriptionType.Free);
            // 2- Create new gym
            int maxRooms = subscription.GetMaxRooms();
            var gym = GymsFactory.CreateGym(maxRooms);
            // 3- Create the maximum number of rooms + 1
            var rooms = new List<Room.Room>();
            for (int i = 1; i <= maxRooms + 1; i++)
            {
                rooms.Add(RoomsFactory.CreateRoom(subscription.GetMaxPlayersPerRoom(), subscription.GetMaxTrainersPerRoom(), roomId: i));
            }

            // Act
            // 1- Add all the rooms
            var addRoomsResults = rooms.ConvertAll(room => gym.AddRoom(room.Id));

            // Assert
            // 1- The operation will success to all rooms but the last room shoud fails.
            var allButLastRoomResults = addRoomsResults[..^1];
            allButLastRoomResults.Should().AllSatisfy(addRoomResult => addRoomResult.Value.Should().Be(Result.Success));

            var lastAddRoomResult = addRoomsResults.Last();
            lastAddRoomResult.IsError.Should().BeTrue();
            lastAddRoomResult.FirstError.Should().Be(GymErrors.CannotHaveMoreRoomsThanTheSubscriptionAllows);
        }
    }
}
