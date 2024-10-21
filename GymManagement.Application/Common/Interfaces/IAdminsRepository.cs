using ErrorOr;
using GymManagement.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface IAdminsRepository
    {
        Task AddAdminAsync(Admin admin);
        Task<Admin?> GetByIdAsync(int id);
        Task UpdateAdminAsync(Admin admin);
        Task<Admin?> GetByUserNameAsync(string userName);
        Task<bool> ExistsBuUserNameAsync(string userName);
    }
}
