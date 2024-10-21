using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Rooms
{
    public record CreateRoomRequest(string name, RoomType roomType, bool isActive, int gymId);
}
