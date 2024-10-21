using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Authentication
{
    public record AuthenticationResponse(int Id, string Name, string UserName, int Roles, string Token);
}
