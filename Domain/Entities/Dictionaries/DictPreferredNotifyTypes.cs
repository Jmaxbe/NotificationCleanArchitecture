namespace Domain.Entities.Dictionaries;

public class DictPreferredNotifyTypes : BaseAuditableEntity
{
    public string Name { get; set; }

    public ICollection<UserPreferences> UserPreferences { get; set; }
}