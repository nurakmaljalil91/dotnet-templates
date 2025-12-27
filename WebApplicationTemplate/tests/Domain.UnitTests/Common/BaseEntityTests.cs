using Domain.Common;

namespace Domain.UnitTests.Common;

public class BaseEntityTests
{
    private sealed class TestEntity : BaseEntity
    {
    }

    [Fact]
    public void Id_CanBeSet()
    {
        var entity = new TestEntity { Id = 42 };

        Assert.Equal(42, entity.Id);
    }
}
