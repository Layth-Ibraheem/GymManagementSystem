using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gym;
using GymManagement.Domain.Room.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Throw;
#nullable disable
namespace GymManagement.Domain.Room
{
    public class Room : Entity
    {
        private readonly int _maxPlayers;
        private readonly int _maxTrainers;
        private readonly List<int> _playersIds = new();
        private readonly List<int> _trainersIds = new();

        public int GymId { get; private set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public RoomType RoomType { get; private set; }
        public bool IsActive { get; private set; }
        public Room(string name,
            RoomType roomType,
            int maxPlayers,
            int maxTrainers,
            bool isActive,
            int gymId,
            int? id = null) : base()
        {
            Id = id ?? Id;
            Name = name;
            RoomType = roomType;
            IsActive = isActive;
            _maxPlayers = maxPlayers;
            _maxTrainers = maxTrainers;
            GymId = gymId;
        }
        public ErrorOr<Success> AddTrainer(int trainerId)
        {
            if (!IsActive)
            {
                return RoomErrors.InactiveRoom;
            }
            if (_trainersIds.Count >= _maxTrainers)
            {
                return RoomErrors.CannotHaveMoreTrainersThanTheSubscriptionAllows;
            }

            _trainersIds.Add(trainerId);
            return Result.Success;
        }
        public ErrorOr<Success> AddPlayer(int playerId)
        {
            if (!IsActive)
            {
                return RoomErrors.InactiveRoom;
            }

            if (_playersIds.Count >= _maxPlayers)
            {
                return RoomErrors.CannotHaveMorePlayersThanTheSubscriptionAllows;
            }

            _playersIds.Add(playerId);
            return Result.Success;
        }
        public ErrorOr<Success> AssignToGym(Gym.Gym gym)
        {
            var addingRoomResult = gym.AddRoom(Id);
            if (addingRoomResult.IsError)
            {
                return addingRoomResult.FirstError;
            }
            GymId = gym.Id;
            return Result.Success;
        }
        public bool HasPlayer(int playerId)
        {
            return _playersIds.Contains(playerId);
        }
        public bool HasTrainer(int trainerId)
        {
            return _trainersIds.Contains(trainerId);
        }
        public void LoadPlayersIds(IEnumerable<int> playerIds)
        {
            _playersIds.Clear();
            _playersIds.AddRange(playerIds);
        }
        public void LoadTrainerIds(IEnumerable<int> trainerIds)
        {
            _trainersIds.Clear();
            _trainersIds.AddRange(trainerIds);
        }
        public void RemovePlayer(int playerId)
        {
            _playersIds.Remove(playerId);
        }
        public void RemoveTrainer(int trainerId)
        {
            _trainersIds.Remove(trainerId);
        }
        public void Deactivate()
        {
            IsActive = false;
            _domainEvents.Add(new RoomDeactivatedEvent(Id));
        }
        private Room()
        {

        }
    }
}
