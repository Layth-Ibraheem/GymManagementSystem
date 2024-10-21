using ErrorOr;
using GymManagement.Domain.Admin;
using GymManagement.Domain.Common;
using GymManagement.Domain.Subscriptions.Events;
using Throw;

#nullable disable
namespace GymManagement.Domain.Subscriptions
{
    public class Subscription : Entity
    {
        public int AdminId { get; private set; }
        private readonly int _maxGyms;
        private readonly List<int> GymIds = new();
        public int Id { get; private set; }
        public SubscriptionType SubscriptionType { get; private set; }
        public bool IsActive { get; private set; }

        public Subscription(SubscriptionType subscriptionType, int adminId, bool isActive, int? subscriptionId = null) : base()
        {
            if (subscriptionId.HasValue)
            {
                Id = subscriptionId.Value;
            }
            SubscriptionType = subscriptionType;
            AdminId = adminId;
            IsActive = isActive;

            _maxGyms = GetMaxGyms();
        }
        public void LoadGymsIds(IEnumerable<int> gymIds)
        {
            GymIds.Clear();
            GymIds.AddRange(gymIds);
        }
        public ErrorOr<Success> AddGym(int gymId)
        {
            if (!IsActive)
            {
                return SubscriptionErrors.InActiveSubscription;
            }
            if (GymIds.Count >= _maxGyms)
            {
                return SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows;
            }
            GymIds.Add(gymId);
            return Result.Success;
        }
        public int GetMaxGyms() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 1,
            nameof(SubscriptionType.Pro) => 3,
            _ => throw new InvalidOperationException()
        };
        public int GetMaxRooms() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 3,
            nameof(SubscriptionType.Pro) => 9,
            _ => throw new InvalidOperationException()
        };
        public int GetMaxPlayersPerRoom() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 3,
            nameof(SubscriptionType.Starter) => 9,
            nameof(SubscriptionType.Pro) => 18,
            _ => throw new InvalidOperationException()
        };
        public int GetMaxTrainersPerRoom() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 3,
            nameof(SubscriptionType.Pro) => 6,
            _ => throw new InvalidOperationException()
        };
        public bool HasGym(int gymId)
        {
            return GymIds.Contains(gymId);
        }
        public void Deactivate()
        {
            IsActive = false;

            _domainEvents.Add(new SubscriptionDeactivatedEvent(Id));
        }
        public void RemoveGym(int gymId)
        {
            GymIds.Throw().IfNotContains(gymId);

            GymIds.Remove(gymId);
        }

        private Subscription()
        {

        }
    }
}
