using GymManagement.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Common.PassowrdHash
{
    public class PassowrdHasher : IPassowrdHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool IsCorrectPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
