using HotelManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Infrastructure.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.RoomId);

            builder.Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(r => r.RoomNumber)
                .IsUnique();

            builder.Property(r => r.Status)
                .HasConversion<string>();

            builder.HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Amenities)
                .WithMany(a => a.Rooms)
                .UsingEntity(j => j.ToTable("room_amenities"));
        }
    }
}
