using System;
using System.Linq;
using Domain.Exceptions;
using Domain.ValueObjects;
using Xunit;

namespace Domain.UnitTests.ValueObjects;

public class ColourTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]

    // If you also want to treat whitespace as default:
    // [InlineData(" ")]
    // [InlineData("\t")]
    // [InlineData("\n")]
    public void Constructor_WhenCodeIsNullOrWhitespace_DefaultsToBlack(string input)
    {
        // Act
        var colour = new Colour(input);

        // Assert
        Assert.Equal("#000000", colour.Code);
        Assert.Equal("#000000", colour.ToString());
    }

    [Fact]
    public void From_WhenCodeIsSupported_ReturnsColour()
    {
        // Act
        var colour = Colour.From("#FFFFFF");

        // Assert
        Assert.NotNull(colour);
        Assert.Equal("#FFFFFF", colour.Code);
        Assert.Equal(Colour.White, colour); // value equality
    }

    [Theory]
    [InlineData("#ffffff")] // case-sensitive in your current implementation
    [InlineData("#000000")] // not in supported list
    [InlineData("FFFFFF")]  // invalid format for your supported list
    [InlineData("not-a-colour")]
    public void From_WhenCodeIsNotSupported_ThrowsUnsupportedColourException(string code)
    {
        // Act
        var ex = Assert.Throws<UnsupportedColourException>(() => Colour.From(code));

        // Assert (light-touch: message/props depend on your exception implementation)
        Assert.NotNull(ex);
    }

    [Fact]
    public void StaticColours_HaveExpectedCodes()
    {
        Assert.Equal("#FFFFFF", Colour.White.Code);
        Assert.Equal("#FF5733", Colour.Red.Code);
        Assert.Equal("#FFC300", Colour.Orange.Code);
        Assert.Equal("#FFFF66", Colour.Yellow.Code);
        Assert.Equal("#CCFF99", Colour.Green.Code);
        Assert.Equal("#6666FF", Colour.Blue.Code);
        Assert.Equal("#9966CC", Colour.Purple.Code);
        Assert.Equal("#999999", Colour.Grey.Code);
    }

    [Fact]
    public void ToString_ReturnsCode()
    {
        var colour = new Colour("#FFFFFF");
        Assert.Equal("#FFFFFF", colour.ToString());
    }

    [Fact]
    public void ImplicitStringConversion_ReturnsCode()
    {
        var colour = new Colour("#FFFFFF");

        string code = colour;

        Assert.Equal("#FFFFFF", code);
    }

    [Fact]
    public void ExplicitColourConversion_FromString_ValidatesSupported()
    {
        // Act
        var colour = (Colour)"#FFFFFF";

        // Assert
        Assert.Equal(Colour.White, colour);
    }

    [Fact]
    public void ExplicitColourConversion_FromString_WhenUnsupported_Throws()
    {
        Assert.Throws<UnsupportedColourException>(() =>
        {
            var _ = (Colour)"#000000";
        });
    }

    [Fact]
    public void ValueEquality_SameCode_AreEqual()
    {
        var a = new Colour("#FFFFFF");
        var b = new Colour("#FFFFFF");

        Assert.Equal(a, b);
        Assert.True(a.Equals(b));
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void ValueEquality_DifferentCode_AreNotEqual()
    {
        var a = new Colour("#FFFFFF");
        var b = new Colour("#FF5733");

        Assert.NotEqual(a, b);
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void SupportedColours_AllAreDistinctAndNonNull()
    {
        // Because SupportedColours is protected, we can validate indirectly using public static colours
        var supported = new[]
        {
            Colour.White,
            Colour.Red,
            Colour.Orange,
            Colour.Yellow,
            Colour.Green,
            Colour.Blue,
            Colour.Purple,
            Colour.Grey
        };

        Assert.All(supported, c => Assert.False(string.IsNullOrWhiteSpace(c.Code)));
        Assert.Equal(supported.Length, supported.Select(x => x.Code).Distinct().Count());
    }
}
