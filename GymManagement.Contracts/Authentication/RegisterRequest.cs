using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Authentication
{
    public record RegisterRequest(string Name,string UserName,string Password, int Roles);
}
