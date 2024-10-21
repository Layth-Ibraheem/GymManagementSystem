using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Room
{
    public class RoomType : SmartEnum<RoomType>
    {
        public static readonly RoomType Boxing = new(nameof(Boxing), 0);
        public static readonly RoomType Kickboxing = new(nameof(Kickboxing), 1);
        public static readonly RoomType Zomba = new(nameof(Zomba), 2);
        public static readonly RoomType Dancing = new(nameof(Dancing), 3);

        public RoomType(string name, int value) : base(name, value)
        {
        }
    }
}
