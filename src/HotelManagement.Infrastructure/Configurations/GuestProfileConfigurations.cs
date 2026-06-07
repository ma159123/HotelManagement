using HotelManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Infrastructure.Configurations
{
    internal class GuestProfileConfigurations : IEntityTypeConfiguration<GuestProfile>
    {
        public void Configure(EntityTypeBuilder<GuestProfile> builder)
        {
            builder.HasKey(r => r.ProfileId);

            builder.Property(r => r.GuestId)
                   .IsRequired();

            builder.Property(r => r.IdType).IsRequired().HasMaxLength(50);

            builder.Property(r => r.IdNumber).IsRequired().HasMaxLength(100);
            builder.Property(r => r.nationality).IsRequired().HasMaxLength(100);

            builder.HasOne(p => p.Guest)
                   .WithOne(g => g.Profile)
                   .HasForeignKey<GuestProfile>(p => p.GuestId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
