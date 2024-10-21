using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Gyms
{
    public record GetGymsRelatedToSubscriptionResponse(int subscriptionId, List<GymResponse> gyms);
}
