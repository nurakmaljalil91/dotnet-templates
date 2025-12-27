using Domain.Common;

namespace Domain.UnitTests.Common;

public class PaginationResultResponseTests
{
    [Fact]
    public void Defaults_AreZeroOrNull()
    {
        var response = new PaginationResultResponse<string>();

        Assert.Null(response.Data);
        Assert.Equal(0, response.Page);
        Assert.Equal(0, response.Total);
    }

    [Fact]
    public void Properties_CanBeSet()
    {
        var response = new PaginationResultResponse<string>
        {
            Data = "data",
            Page = 2,
            Total = 50
        };

        Assert.Equal("data", response.Data);
        Assert.Equal(2, response.Page);
        Assert.Equal(50, response.Total);
    }
}
