using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Gyms
{
    public record GymResponse(int gymId, string name, int subscriptionId, bool isActive);
}
