using GymManagement.Domain.Player;
using GymManagement.Domain.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Players.Persistence
{
    public class PlayerConfigurations : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Height)
                .HasComment("The Height Is In CM");

            builder.Property(t => t.Weight)
                .HasComment("The Weight Is In KG");


            builder.HasOne<Room>()
                .WithMany()
                .HasForeignKey(p => p.RoomId);
        }
    }
}
