using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Common.Interfaces
{
    public interface IPassowrdHasher
    {
        string HashPassword(string password);
        bool IsCorrectPassword(string password, string hash);
    }
}
