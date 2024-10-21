using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Room
{
    public static class RoomErrors
    {
        public static readonly Error CannotHaveMoreTrainersThanTheSubscriptionAllows = Error.Validation(
            code: "Rooms.CannotHaveMoreTrainersThanTheSubscriptionAllows",
            description: "A room cannot have more trainers than the subscription allows");

        public static readonly Error CannotHaveMorePlayersThanTheSubscriptionAllows = Error.Validation(
            code: "Rooms.CannotHaveMorePlayersThanTheSubscriptionAllows",
            description: "A room cannot have more players than the subscription allows");

        public static readonly Error InactiveRoom = Error.Validation(
            code: "Rooms.InactiveRoom",
            description: "This room is deactivated");
    }
}
