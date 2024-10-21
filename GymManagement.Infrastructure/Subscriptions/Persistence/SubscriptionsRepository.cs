using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Subscriptions.Persistence
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly GymManagementDbContext _context;

        public SubscriptionsRepository(GymManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
        }

        public Task UpdateSubscriptionAsync(Subscription subscription)
        {
            _context.Update(subscription);

            return Task.CompletedTask;
        }

        public async Task<Subscription?> GetByIdAsync(int Id)
        {
            var subscription = await _context.Subscriptions.FindAsync(Id);

            return subscription;
        }

        public async Task<List<Subscription>?> ListByAdminIdAsync(int adminId)
        {
            var subscriptions = await _context.Subscriptions.Where(s => s.AdminId == adminId).ToListAsync();
            return subscriptions;
        }
    }
}
