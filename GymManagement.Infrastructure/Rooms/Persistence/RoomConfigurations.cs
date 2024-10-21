using GymManagement.Domain.Gym;
using GymManagement.Domain.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Rooms.Persistence
{
    public class RoomConfigurations : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder.Property(r => r.Name)
                .IsRequired(required: true)
                .HasMaxLength(50);

            //builder.Ignore(r => r.PlayersIds);
            //builder.Ignore(r => r.TrainersIds);


            builder.Property(r => r.RoomType)
                .HasConversion(roomType => roomType.Value,
                value => RoomType.FromValue(value))
                .HasComment($"{RoomType.Boxing.Value}: {RoomType.Boxing.Name}, " +
                $"{RoomType.Kickboxing.Value}: {RoomType.Kickboxing.Name}, " +
                $"{RoomType.Zomba.Value}: {RoomType.Zomba.Name}, " +
                $"{RoomType.Dancing.Value}: {RoomType.Dancing.Name} ")
                .IsRequired(required: true);

            builder.HasOne<Gym>()
                .WithMany()
                .HasForeignKey(r => r.GymId);

            builder.Property("_maxPlayers").HasColumnName("MaxPlayers");
            builder.Property("_maxTrainers").HasColumnName("MaxTriners");

        }
    }
}
