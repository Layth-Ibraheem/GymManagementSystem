using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Gym
{
    public static class GymErrors
    {
        public static readonly Error CannotHaveMoreRoomsThanTheSubscriptionAllows = Error.Validation(
            code: "Gyms.CannotHaveMoreRoomsThanTheSubscriptionAllows",
            description: "A gym cannot have more rooms than the subscription allows");

        public static readonly Error InactiveGym = Error.Validation(
            code: "Gyms.InactiveGym",
            description: "This gym is deactivated");
    }
}
