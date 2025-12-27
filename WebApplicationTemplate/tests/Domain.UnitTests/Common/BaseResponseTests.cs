using System.Collections.Generic;
using Domain.Common;

namespace Domain.UnitTests.Common;

public class BaseResponseTests
{
    [Fact]
    public void Ok_SetsSuccessDataAndMessage()
    {
        var payload = new { Id = 1 };

        var response = BaseResponse<object>.Ok(payload, "ok");

        Assert.True(response.Success);
        Assert.Equal("ok", response.Message);
        Assert.Same(payload, response.Data);
        Assert.Null(response.Errors);
    }

    [Fact]
    public void Fail_SetsFailureMessageAndErrors()
    {
        var errors = new Dictionary<string, string[]>
        {
            ["Title"] = new[] { "Required" }
        };

        var response = BaseResponse<object>.Fail("bad", errors);

        Assert.False(response.Success);
        Assert.Equal("bad", response.Message);
        Assert.Same(errors, response.Errors);
        Assert.Null(response.Data);
    }
}
