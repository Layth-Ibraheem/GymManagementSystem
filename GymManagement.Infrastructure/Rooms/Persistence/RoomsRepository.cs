using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Room;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Rooms.Persistence
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly GymManagementDbContext _context;

        public RoomsRepository(GymManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddRoomAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }

        public async Task<List<Room>> ListByGymIdAsync(int gymId)
        {
            return await _context.Rooms.AsNoTracking().Where(r => r.GymId == gymId).ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public Task UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
            return Task.CompletedTask;
        }
    }
}
