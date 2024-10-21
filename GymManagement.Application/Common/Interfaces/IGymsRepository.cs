using GymManagement.Domain.Gym;
using GymClass = GymManagement.Domain.Gym.Gym;
namespace GymManagement.Application.Common.Interfaces
{
    public interface IGymsRepository
    {
        Task<GymClass?> GetByIdAsync(int gymId);
        Task<bool> ExistsAsync(int id);
        Task<List<GymClass>> ListBySubscriptionIdAsync(int subscriptionId);
        Task UpdateGymAsync(GymClass gym);
        Task RemoveGymAsync(GymClass gym);
        Task RemoveRangeAsync(List<GymClass> gyms);

        Task AddGymAsync(Gym gym);
    }
}
