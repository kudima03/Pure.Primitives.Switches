using Pure.HashCodes;
using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.Primitives.Switches.Date;

namespace Pure.Primitives.Switches.Tests.Date;

using Date = Primitives.Date.Date;
using String = String.String;

public sealed record DateSwitchTests
{
    [Fact]
    public void FindRequiredBranch()
    {
        IDate dateSwitch = new DateSwitch<IString>(
            new String("branch4"),
            [
                new KeyValuePair<IString, IDate>(
                    new String("branch1"),
                    new Date(new UShort(1), new UShort(1), new UShort(2000))
                ),
                new KeyValuePair<IString, IDate>(
                    new String("branch2"),
                    new Date(new UShort(1), new UShort(1), new UShort(2001))
                ),
                new KeyValuePair<IString, IDate>(
                    new String("branch3"),
                    new Date(new UShort(1), new UShort(1), new UShort(2002))
                ),
                new KeyValuePair<IString, IDate>(
                    new String("branch4"),
                    new Date(new UShort(1), new UShort(1), new UShort(2003))
                ),
            ],
            x => new DeterminedHash(x)
        );

        Assert.Equal(
            new DeterminedHash(
                new Date(new UShort(1), new UShort(1), new UShort(2003))
            ).AsEnumerable(),
            new DeterminedHash(dateSwitch).AsEnumerable()
        );
    }

    [Fact]
    public void GiveDefaultValueOnNotExistingSelector()
    {
        IDate dateSwitch = new DateSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IDate>(
                    new String("branch1"),
                    new Date(new UShort(1), new UShort(1), new UShort(2000))
                ),
                new KeyValuePair<IString, IDate>(
                    new String("branch2"),
                    new Date(new UShort(1), new UShort(1), new UShort(2001))
                ),
                new KeyValuePair<IString, IDate>(
                    new String("branch3"),
                    new Date(new UShort(1), new UShort(1), new UShort(2002))
                ),
                new KeyValuePair<IString, IDate>(
                    new String("branch4"),
                    new Date(new UShort(1), new UShort(1), new UShort(2003))
                ),
            ],
            x => new DeterminedHash(x),
            new Date(new UShort(1), new UShort(1), new UShort(2004))
        );

        Assert.Equal(
            new DeterminedHash(
                new Date(new UShort(1), new UShort(1), new UShort(2004))
            ).AsEnumerable(),
            new DeterminedHash(dateSwitch).AsEnumerable()
        );
    }

    [Fact]
    public void ThrowsExceptionOnNotExistingSelectorWithNoDefaultValue()
    {
        IDate dateSwitch = new DateSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IDate>(
                    new String("branch1"),
                    new Date(new UShort(1), new UShort(1), new UShort(2000))
                ),
                new KeyValuePair<IString, IDate>(
                    new String("branch2"),
                    new Date(new UShort(1), new UShort(1), new UShort(2001))
                ),
                new KeyValuePair<IString, IDate>(
                    new String("branch3"),
                    new Date(new UShort(1), new UShort(1), new UShort(2002))
                ),
                new KeyValuePair<IString, IDate>(
                    new String("branch4"),
                    new Date(new UShort(1), new UShort(1), new UShort(2003))
                ),
            ],
            x => new DeterminedHash(x)
        );

        _ = Assert.Throws<InvalidOperationException>(() =>
            new DeterminedHash(dateSwitch).ToArray()
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new DateSwitch<IString>(
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
            new DateSwitch<IString>(
                new RandomString(),
                [],
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
