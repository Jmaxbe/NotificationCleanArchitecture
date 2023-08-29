using Domain.Entities.Dictionaries;

namespace Domain.Entities;

public class UserPreferences : BaseAuditableEntity
{
    public Guid ExternalUserId { get; set; }
    public Guid DictPreferredNotifyTypesId { get; set; }
    public DictPreferredNotifyTypes DictPreferredNotifyTypes { get; set; }
}