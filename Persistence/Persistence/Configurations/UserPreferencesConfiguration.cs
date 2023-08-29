using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Persistence.Configurations;

public class UserPreferencesConfiguration : IEntityTypeConfiguration<UserPreferences>
{
    public void Configure(EntityTypeBuilder<UserPreferences> builder)
    {
        builder.Property(p => p.ExternalUserId)
            .IsRequired();
        builder
            .HasOne(o => o.DictPreferredNotifyTypes)
            .WithMany(m => m.UserPreferences)
            .HasForeignKey(f => f.DictPreferredNotifyTypesId)
            .HasPrincipalKey(g => g.UniqueId);
    }
}