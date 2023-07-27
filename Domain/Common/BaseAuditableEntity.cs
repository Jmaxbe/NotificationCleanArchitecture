using System.ComponentModel.DataAnnotations;

namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }

    [MaxLength(256)]
    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    [MaxLength(256)]
    public string? LastModifiedBy { get; set; }
    
    [Timestamp]
    public byte[] RowVersion { get; set; }
}