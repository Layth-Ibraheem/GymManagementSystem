using GymManagement.Domain.Trainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface ITrainersRepository
    {
        Task AddTrainerAsync(Trainer entity);
        Task UpdateTrainerAsync(Trainer entity);
        Task<Trainer?> GetTrainerByIdAsync(int id);
        Task<List<Trainer>> ListByRoomIdAsync(int roomId);
    }
}
