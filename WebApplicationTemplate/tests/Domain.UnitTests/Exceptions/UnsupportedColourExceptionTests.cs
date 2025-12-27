using Domain.Exceptions;

namespace Domain.UnitTests.Exceptions;

public class UnsupportedColourExceptionTests
{
    [Fact]
    public void Message_IncludesCode()
    {
        var ex = new UnsupportedColourException("#123456");

        Assert.Equal("Colour \"#123456\" is unsupported.", ex.Message);
    }
}
