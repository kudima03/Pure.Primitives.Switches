using Pure.HashCodes;
using Pure.Primitives.Abstractions.DateTime;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.Primitives.Switches.DateTime;

namespace Pure.Primitives.Switches.Tests.DateTime;

using Date = Primitives.Date.Date;
using DateTime = Primitives.DateTime.DateTime;
using String = Pure.Primitives.String.String;
using Time = Primitives.Time.Time;

public sealed record DateTimeSwitchTests
{
    [Fact]
    public void FindRequiredBranch()
    {
        IDateTime dateTimeSwitch = new DateTimeSwitch<IString>(
            new String("branch4"),
            [
                new KeyValuePair<IString, IDateTime>(
                    new String("branch1"),
                    new DateTime(
                        new Date(new UShort(1), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
                new KeyValuePair<IString, IDateTime>(
                    new String("branch2"),
                    new DateTime(
                        new Date(new UShort(2), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
                new KeyValuePair<IString, IDateTime>(
                    new String("branch3"),
                    new DateTime(
                        new Date(new UShort(3), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
                new KeyValuePair<IString, IDateTime>(
                    new String("branch4"),
                    new DateTime(
                        new Date(new UShort(4), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
            ],
            x => new DeterminedHash(x)
        );

        Assert.Equal(
            new DeterminedHash(
                new DateTime(
                    new Date(new UShort(4), new UShort(1), new UShort(2000)),
                    new Time(new UShort(23), new UShort(12))
                )
            ).AsEnumerable(),
            new DeterminedHash(dateTimeSwitch).AsEnumerable()
        );
    }

    [Fact]
    public void GiveDefaultValueOnNotExistingSelector()
    {
        IDateTime dateTimeSwitch = new DateTimeSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IDateTime>(
                    new String("branch1"),
                    new DateTime(
                        new Date(new UShort(1), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
                new KeyValuePair<IString, IDateTime>(
                    new String("branch2"),
                    new DateTime(
                        new Date(new UShort(2), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
                new KeyValuePair<IString, IDateTime>(
                    new String("branch3"),
                    new DateTime(
                        new Date(new UShort(3), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
                new KeyValuePair<IString, IDateTime>(
                    new String("branch4"),
                    new DateTime(
                        new Date(new UShort(4), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
            ],
            x => new DeterminedHash(x),
            new DateTime(
                new Date(new UShort(5), new UShort(1), new UShort(2000)),
                new Time(new UShort(23), new UShort(12))
            )
        );

        Assert.Equal(
            new DeterminedHash(
                new DateTime(
                    new Date(new UShort(5), new UShort(1), new UShort(2000)),
                    new Time(new UShort(23), new UShort(12))
                )
            ).AsEnumerable(),
            new DeterminedHash(dateTimeSwitch).AsEnumerable()
        );
    }

    [Fact]
    public void ThrowsExceptionOnNotExistingSelectorWithNoDefaultValue()
    {
        IDateTime dateTimeSwitch = new DateTimeSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IDateTime>(
                    new String("branch1"),
                    new DateTime(
                        new Date(new UShort(1), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
                new KeyValuePair<IString, IDateTime>(
                    new String("branch2"),
                    new DateTime(
                        new Date(new UShort(2), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
                new KeyValuePair<IString, IDateTime>(
                    new String("branch3"),
                    new DateTime(
                        new Date(new UShort(3), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
                new KeyValuePair<IString, IDateTime>(
                    new String("branch4"),
                    new DateTime(
                        new Date(new UShort(4), new UShort(1), new UShort(2000)),
                        new Time(new UShort(23), new UShort(12))
                    )
                ),
            ],
            x => new DeterminedHash(x)
        );

        _ = Assert.Throws<InvalidOperationException>(() =>
            new DeterminedHash(dateTimeSwitch).ToArray()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new DateTimeSwitch<IString>(
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
            new DateTimeSwitch<IString>(
                new RandomString(),
                [],
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
