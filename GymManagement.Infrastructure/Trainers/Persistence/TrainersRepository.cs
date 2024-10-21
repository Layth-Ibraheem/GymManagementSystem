using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Trainer;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Trainers.Persistence
{
    public class TrainersRepository : ITrainersRepository
    {
        private readonly GymManagementDbContext _context;

        public TrainersRepository(GymManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddTrainerAsync(Trainer entity)
        {
            await _context.Trainers.AddAsync(entity);
        }

        public async Task<Trainer?> GetTrainerByIdAsync(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public Task UpdateTrainerAsync(Trainer entity)
        {
            _context.Trainers.Update(entity);
            return Task.CompletedTask;
        }
        public async Task<List<Trainer>> ListByRoomIdAsync(int roomId)
        {
            return await _context.Trainers
                .AsNoTracking()
                .Where(t => t.RoomId == roomId)
                .ToListAsync();
        }
    }
}
