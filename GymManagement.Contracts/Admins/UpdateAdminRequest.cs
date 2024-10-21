using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Admins
{
    public record UpdateAdminRequest(int adminId, string name, string password);

}
