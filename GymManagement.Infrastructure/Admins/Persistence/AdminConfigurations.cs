using GymManagement.Domain.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Admins.Persistence
{
    public class AdminConfigurations : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Name)
                .IsRequired(required: true)
                .HasMaxLength(50);

            builder.Property(a => a.UserName)
                .IsRequired(required: true)
                .HasMaxLength(50);

            builder.Property("_hashedPassowrd")
                .HasColumnName("Password")
                .IsRequired(required: true);

            builder.Property(a => a.Roles)
                .IsRequired(true);

            //builder.Ignore(a => a.SubscriptionIds);
        }
    }
}
