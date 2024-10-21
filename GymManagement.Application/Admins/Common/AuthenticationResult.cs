using GymManagement.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Admins.Common
{
    public record AuthenticationResult(Admin admin, string token);
}
