using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Admins
{
    public record CreateAdminRequest(string name, string userName, string password, bool isActive, int rolls);

}
