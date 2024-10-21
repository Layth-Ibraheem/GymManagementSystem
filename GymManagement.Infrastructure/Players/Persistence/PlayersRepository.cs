using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Player;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Players.Persistence
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly GymManagementDbContext _context;

        public PlayersRepository(GymManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddPlayerAsync(Player entity)
        {
            await _context.Players.AddAsync(entity);
        }

        public async Task<Player?> GetPlayerByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        public Task UpdatePlayerAsync(Player entity)
        {
            _context.Players.Update(entity);

            return Task.CompletedTask;
        }

        public async Task<List<Player>> ListByRoomIdAsync(int roomId)
        {
            return await _context.Players
                .AsNoTracking()
                .Where(p => p.RoomId == roomId)
                .ToListAsync();
        }
    }
}
