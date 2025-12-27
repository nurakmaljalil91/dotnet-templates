using Domain.Common;

namespace Domain.UnitTests.Common;

public class ApiStatusTests
{
    [Fact]
    public void Properties_CanBeSet()
    {
        var status = new ApiStatus
        {
            Status = "OK",
            BuildVersion = "1.0.0"
        };

        Assert.Equal("OK", status.Status);
        Assert.Equal("1.0.0", status.BuildVersion);
    }
}
