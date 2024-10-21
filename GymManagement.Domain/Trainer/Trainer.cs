#nullable disable

using ErrorOr;
using GymManagement.Domain.Room;

namespace GymManagement.Domain.Trainer
{
    public class Trainer
    {
        public int RoomId { get; private set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public short Height { get; private set; }
        public short Weight { get; private set; }
        public bool IsActive { get; private set; }
        public Trainer(string name, bool isActive, short height, short width, int roomId, int? id = null)
        {
            Id = id ?? Id;
            Name = name;
            IsActive = isActive;
            Height = height;
            Weight = width;
            RoomId = roomId;
        }
        public ErrorOr<Success> AssignToRoom(Room.Room room)
        {
            var addingTrainerResult = room.AddTrainer(Id);
            if (addingTrainerResult.IsError)
            {
                return addingTrainerResult.FirstError;
            }
            RoomId = room.Id;
            return Result.Success;
        }
        public void Deactivate()
        {
            IsActive = false;
        }
        private Trainer()
        {

        }
    }
}
