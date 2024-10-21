using GymManagement.Domain.Room;
using GymManagement.Domain.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Trainers.Persistence
{
    public class TrainerConfigurations : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
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
                .HasForeignKey(t => t.RoomId);
        }
    }
}
