using GymManagement.Domain.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface IRoomsRepository
    {
        Task AddRoomAsync(Room room);
        Task UpdateRoom(Room room);
        Task<Room?> GetByIdAsync(int id);
        Task<List<Room>> ListByGymIdAsync(int gymId);
    }
}
