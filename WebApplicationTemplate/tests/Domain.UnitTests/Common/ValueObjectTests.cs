using Domain.Common;

namespace Domain.UnitTests.Common;

public class ValueObjectTests
{
    private sealed class SampleValueObject : ValueObject
    {
        public SampleValueObject(int number, string? text)
        {
            Number = number;
            Text = text;
        }

        public int Number { get; }
        public string? Text { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
            yield return Text!;
        }

        public static bool operator ==(SampleValueObject left, SampleValueObject right)
            => EqualOperator(left, right);

        public static bool operator !=(SampleValueObject left, SampleValueObject right)
            => NotEqualOperator(left, right);
    }

    [Fact]
    public void Equals_SameComponents_AreEqual()
    {
        var a = new SampleValueObject(1, "a");
        var b = new SampleValueObject(1, "a");

        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Equals_DifferentComponents_AreNotEqual()
    {
        var a = new SampleValueObject(1, "a");
        var b = new SampleValueObject(2, "a");

        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void Equals_NullHandling_Works()
    {
        SampleValueObject? a = null;
        SampleValueObject? b = null;
        var c = new SampleValueObject(1, "a");

        Assert.True(a == b);
        Assert.True(a != c);
    }
}
