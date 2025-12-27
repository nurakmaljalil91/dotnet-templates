using Domain.Common;
using NodaTime;

namespace Domain.UnitTests.Common;

public class BaseAuditableEntityTests
{
    private sealed class TestAuditableEntity : BaseAuditableEntity
    {
    }

    [Fact]
    public void Properties_CanBeSet()
    {
        var created = Instant.FromUtc(2025, 1, 1, 0, 0);
        var updated = Instant.FromUtc(2025, 1, 2, 0, 0);

        var entity = new TestAuditableEntity
        {
            Id = 7,
            CreatedDate = created,
            CreatedBy = "creator",
            UpdatedDate = updated,
            UpdatedBy = "updater"
        };

        Assert.Equal(7, entity.Id);
        Assert.Equal(created, entity.CreatedDate);
        Assert.Equal("creator", entity.CreatedBy);
        Assert.Equal(updated, entity.UpdatedDate);
        Assert.Equal("updater", entity.UpdatedBy);
    }
}
