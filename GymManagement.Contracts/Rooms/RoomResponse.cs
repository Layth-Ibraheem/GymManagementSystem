using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Rooms
{
    public record RoomResponse(int roomId, RoomType roomType, string name, bool isActive, int gymId);
}
