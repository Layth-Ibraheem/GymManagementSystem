using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gym.Events;
using GymManagement.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Throw;
#nullable disable

namespace GymManagement.Domain.Gym
{
    public class Gym : Entity
    {
        private readonly int _maxRooms;
        private readonly List<int> _roomsIds = new();
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int SubscriptionId { get; private set; }
        public bool IsActive { get; private set; }
        public Gym(string name, int subscriptionId, int maxRooms, bool isActive, int? gymId = null) : base()
        {
            if (gymId.HasValue)
            {
                Id = gymId.Value;
            }
            Name = name;
            SubscriptionId = subscriptionId;
            IsActive = isActive;

            _maxRooms = maxRooms;

        }
        public void LoadRoomsIds(IEnumerable<int> roomsIds)
        {
            _roomsIds.Clear();
            _roomsIds.AddRange(roomsIds);
        }
        public ErrorOr<Success> AddRoom(int roomId)
        {
            if (!IsActive)
            {
                return GymErrors.InactiveGym;
            }
            if (_roomsIds.Count >= _maxRooms)
            {
                return GymErrors.CannotHaveMoreRoomsThanTheSubscriptionAllows;
            }

            _roomsIds.Add(roomId);
            return Result.Success;
        }
        public void RemoveRoom(int roomId)
        {
            _roomsIds.Remove(roomId);
        }
        public bool HasRoom(int roomId)
        {
            return _roomsIds.Contains(roomId);
        }
        public ErrorOr<Success> AssignToSubscription(Subscription subscription)
        {
            var addGymResult = subscription.AddGym(Id);
            if (addGymResult.IsError)
            {
                return addGymResult.FirstError;
            }
            SubscriptionId = subscription.Id;
            return Result.Success;
        }
        public void Deactivate()
        {
            IsActive = false;
            _domainEvents.Add(new GymDeactivatedEvent(Id));
        }
        private Gym()
        {

        }
    }
}
