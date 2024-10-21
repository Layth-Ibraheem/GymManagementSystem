#nullable disable

using ErrorOr;

namespace GymManagement.Domain.Player
{
    public class Player
    {
        public int RoomId { get; private set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public short Height { get; set; }
        public short Weight { get; set; }
        public bool IsActive { get; set; }
        public Player(string name, short height, short weight, bool isActive, int roomId, int? id = null)
        {
            Id = id ?? Id;
            Name = name;
            Height = height;
            Weight = weight;
            IsActive = isActive;
            RoomId = roomId;
        }
        public ErrorOr<Success> AssignToRoom(Room.Room room)
        {
            var addingPlayerToRoomResult = room.AddPlayer(Id);
            if (addingPlayerToRoomResult.IsError)
            {
                return addingPlayerToRoomResult.FirstError;
            }
            RoomId = room.Id;
            return Result.Success;
        }
        public void Deactivate()
        {
            IsActive = false;
        }
        private Player()
        {

        }
    }
}
