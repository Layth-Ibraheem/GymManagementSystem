using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gym;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms.Persistence
{
    public class GymsRepository : IGymsRepository
    {
        private readonly GymManagementDbContext _context;

        public GymsRepository(GymManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddGymAsync(Gym gym)
        {
            await _context.Gyms.AddAsync(gym);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Gyms
                .AsNoTracking()
                .AnyAsync(g => g.Id == id);
        }

        public async Task<Gym?> GetByIdAsync(int gymId)
        {
            return await _context.Gyms.FindAsync(gymId);
        }

        public async Task<List<Gym>> ListBySubscriptionIdAsync(int subscriptionId)
        {
            return await _context.Gyms.AsNoTracking().Where(g => g.SubscriptionId == subscriptionId).ToListAsync();
        }

        public Task RemoveGymAsync(Gym gym)
        {
            // To Do: Create IsActive Property in gyms and set it to false (soft delete)
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(List<Gym> gyms)
        {
            // To Do: Create IsActive Property in gyms and set it to false (soft delete)
            return Task.CompletedTask;
        }

        public Task UpdateGymAsync(Gym gym)
        {
            _context.Update(gym);
            return Task.CompletedTask;
        }
    }
}
