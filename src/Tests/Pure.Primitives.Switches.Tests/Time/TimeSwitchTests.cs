using Pure.HashCodes;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Abstractions.Time;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.Primitives.Switches.Time;

namespace Pure.Primitives.Switches.Tests.Time;

using String = Primitives.String.String;
using Time = Primitives.Time.Time;

public sealed record TimeSwitchTests
{
    [Fact]
    public void FindRequiredBranch()
    {
        ITime timeSwitch = new TimeSwitch<IString>(
            new String("branch4"),
            [
                new KeyValuePair<IString, ITime>(
                    new String("branch1"),
                    new Time(new UShort(24), new UShort(12))
                ),
                new KeyValuePair<IString, ITime>(
                    new String("branch2"),
                    new Time(new UShort(23), new UShort(12))
                ),
                new KeyValuePair<IString, ITime>(
                    new String("branch3"),
                    new Time(new UShort(22), new UShort(12))
                ),
                new KeyValuePair<IString, ITime>(
                    new String("branch4"),
                    new Time(new UShort(21), new UShort(12))
                ),
            ],
            x => new DeterminedHash(x)
        );

        Assert.Equal(
            new DeterminedHash(new Time(new UShort(21), new UShort(12))).AsEnumerable(),
            new DeterminedHash(timeSwitch).AsEnumerable()
        );
    }

    [Fact]
    public void GiveDefaultValueOnNotExistingSelector()
    {
        ITime timeSwitch = new TimeSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, ITime>(
                    new String("branch1"),
                    new Time(new UShort(24), new UShort(12))
                ),
                new KeyValuePair<IString, ITime>(
                    new String("branch2"),
                    new Time(new UShort(23), new UShort(12))
                ),
                new KeyValuePair<IString, ITime>(
                    new String("branch3"),
                    new Time(new UShort(22), new UShort(12))
                ),
                new KeyValuePair<IString, ITime>(
                    new String("branch4"),
                    new Time(new UShort(21), new UShort(12))
                ),
            ],
            x => new DeterminedHash(x),
            new Time(new UShort(20), new UShort(12))
        );

        Assert.Equal(
            new DeterminedHash(new Time(new UShort(20), new UShort(12))).AsEnumerable(),
            new DeterminedHash(timeSwitch).AsEnumerable()
        );
    }

    [Fact]
    public void ThrowsExceptionOnNotExistingSelectorWithNoDefaultValue()
    {
        ITime timeSwitch = new TimeSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, ITime>(
                    new String("branch1"),
                    new Time(new UShort(24), new UShort(12))
                ),
                new KeyValuePair<IString, ITime>(
                    new String("branch2"),
                    new Time(new UShort(23), new UShort(12))
                ),
                new KeyValuePair<IString, ITime>(
                    new String("branch3"),
                    new Time(new UShort(22), new UShort(12))
                ),
                new KeyValuePair<IString, ITime>(
                    new String("branch4"),
                    new Time(new UShort(21), new UShort(12))
                ),
            ],
            x => new DeterminedHash(x)
        );

        _ = Assert.Throws<InvalidOperationException>(() =>
            new DeterminedHash(timeSwitch).ToArray()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new TimeSwitch<IString>(
                new RandomString(),
                [],
                x => new DeterminedHash(x)
            ).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new TimeSwitch<IString>(
                new RandomString(),
                [],
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
