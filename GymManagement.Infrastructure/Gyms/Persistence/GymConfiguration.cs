using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GymClass = GymManagement.Domain.Gym.Gym;
namespace GymManagement.Infrastructure.Gyms.Persistence
{
    public class GymConfiguration : IEntityTypeConfiguration<GymClass>
    {
        public void Configure(EntityTypeBuilder<GymClass> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(50).IsRequired(required: true);

            //builder.Ignore(g => g.RoomsIds);

            builder.HasOne<Subscription>()
                .WithMany()
                .HasForeignKey(g => g.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property("_maxRooms").HasColumnName("MaxRooms");
        }
    }
}
