using GymManagement.Domain.Admin;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Subscriptions.Persistence
{
    public class SubscriptionConfigurations : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //builder.Ignore(s => s.GymIds);

            builder.Property("_maxGyms").HasColumnName("MaxGyms")
                .IsRequired(required: true)
                .HasDefaultValue(1);

            builder.HasOne<Admin>()
                .WithOne()
                .HasForeignKey<Subscription>(s => s.AdminId);

            builder.Property(s => s.SubscriptionType)
                .HasConversion(subscriptionType => subscriptionType.Value,
                value => SubscriptionType.FromValue(value))
                .HasComment(
                $"{SubscriptionType.Free.Value}: {SubscriptionType.Free.Name}," +
                $" {SubscriptionType.Starter.Value}: {SubscriptionType.Starter.Name}," +
                $" {SubscriptionType.Pro.Value}: {SubscriptionType.Pro.Name}")
                .IsRequired(required: true);
        }
    }
}
