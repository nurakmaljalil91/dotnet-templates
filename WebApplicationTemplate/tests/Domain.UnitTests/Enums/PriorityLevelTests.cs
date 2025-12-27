using Domain.Enums;

namespace Domain.UnitTests.Enums;

public class PriorityLevelTests
{
    [Fact]
    public void Values_AreExpected()
    {
        Assert.Equal(0, (int)PriorityLevel.None);
        Assert.Equal(1, (int)PriorityLevel.Low);
        Assert.Equal(2, (int)PriorityLevel.Medium);
        Assert.Equal(3, (int)PriorityLevel.High);
    }
}
