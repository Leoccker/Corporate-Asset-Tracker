using AssetTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetTracker.Infrastructure.Persistence.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.ToTable("Assets");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.SerialNumber)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.HasIndex(a => a.SerialNumber)
                .IsUnique();

            builder.Property(a => a.TagNumber)
                .HasMaxLength(20);

            builder.Property(a => a.Status)
                .HasConversion<string>();
        }
    }
}
