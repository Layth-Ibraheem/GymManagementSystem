using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Admins
{
    public record AdminResponse(int id, string name, string userName, bool isActive, int rolls);
}
