using Domain.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Persistence.Configurations;

public class DictPreferredNotifyTypesConfiguration : IEntityTypeConfiguration<DictPreferredNotifyTypes>
{
    public void Configure(EntityTypeBuilder<DictPreferredNotifyTypes> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(256);
    }
}