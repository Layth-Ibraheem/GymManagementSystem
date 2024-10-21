using GymManagement.Domain.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface IPlayersRepository
    {
        Task AddPlayerAsync(Player player);
        Task UpdatePlayerAsync(Player player);
        Task<Player?> GetPlayerByIdAsync(int id);
        Task<List<Player>> ListByRoomIdAsync(int roomId);
    }
}
