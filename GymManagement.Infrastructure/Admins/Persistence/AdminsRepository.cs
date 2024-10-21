using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Admins.Persistence
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly GymManagementDbContext _context;

        public AdminsRepository(GymManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddAdminAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
        }

        public async Task<bool> ExistsBuUserNameAsync(string userName)
        {
            return await _context.Admins.AnyAsync(admin => admin.UserName == userName);
        }

        public async Task<Admin?> GetByIdAsync(int id)
        {
            return await _context.Admins.FindAsync(id);

            //return await _context.Admins
            //  .GroupJoin(
            //      _context.Subscriptions, // join Admins with Subscriptions
            //      admin => admin.Id,       // outer key selector (Admin.Id)
            //      subscription => subscription.AdminId, // inner key selector (Subscription.AdminId)
            //      (admin, subscriptions) => new { Admin = admin, Subscriptions = subscriptions.DefaultIfEmpty() }) // left join
            //  .SelectMany(
            //      result => result.Subscriptions, // flatten the result set
            //      (result, subscription) => new { result.Admin, Subscription = subscription }) // project the result
            //  .Where(result => result.Admin.Id == id) // filter by the admin ID
            //  .Select(result => new Admin(
            //      result.Admin.Name,
            //      result.Admin.Password,
            //      result.Admin.UserName,
            //      result.Admin.IsActive,
            //      result.Subscription != null ? result.Subscription.Id : null, // if subscription exists, assign its Id; otherwise null
            //      result.Admin.Id))
            //  .FirstOrDefaultAsync();


        }

        public async Task<Admin?> GetByUserNameAsync(string userName)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.UserName ==  userName);
        }

        public Task UpdateAdminAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            return Task.CompletedTask;
        }
    }
}
