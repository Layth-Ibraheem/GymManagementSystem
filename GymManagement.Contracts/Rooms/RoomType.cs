using System.Text.Json.Serialization;

namespace GymManagement.Contracts.Rooms
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum RoomType
    {
        Boxing = 0,
        Kickboxing,
        Zomba,
        Dancing
    }
}
