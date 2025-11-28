using NodaTime;

namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public Instant CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public Instant UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
}

