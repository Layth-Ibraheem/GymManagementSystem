using GymManagement.Domain.Common;

namespace GymManagement.Domain.Gym.Events
{
    public record GymDeactivatedEvent(int gymId) : IDomainEvent;
}
