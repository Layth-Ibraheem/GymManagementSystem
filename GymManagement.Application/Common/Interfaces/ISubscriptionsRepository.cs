using ErrorOr;
using GymManagement.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface ISubscriptionsRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);
        Task<Subscription?> GetByIdAsync(int Id);
        Task<List<Subscription>?> ListByAdminIdAsync(int adminId);
        Task UpdateSubscriptionAsync(Subscription subscription);

    }
}
