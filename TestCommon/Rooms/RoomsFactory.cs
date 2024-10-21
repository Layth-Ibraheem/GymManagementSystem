using GymManagement.Domain.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.TestConstants;

namespace TestCommon.Rooms
{
    public static class RoomsFactory
    {
        public static Room CreateRoom(
            int maxPlayers,
            int maxTrainers,
            string? name = null,
            RoomType? roomType = null,
            bool? isActive = null,
            int? gymId = null,
            int? roomId = null)
        {
            return new Room(
                name ?? Constants.Room.Name,
                roomType ?? Constants.Room.DefaultRoomType,
                maxPlayers,
                maxTrainers,
                isActive ?? Constants.Room.IsActive,
                gymId ?? Constants.Gym.GymId,
                roomId ?? Constants.Room.RoomId);
        }
    }
}
