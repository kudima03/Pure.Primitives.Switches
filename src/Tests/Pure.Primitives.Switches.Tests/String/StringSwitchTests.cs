using Pure.HashCodes;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Pure.Primitives.Switches.String;

namespace Pure.Primitives.Switches.Tests.String;

using String = Primitives.String.String;

public sealed record StringSwitchTests
{
    [Fact]
    public void FindRequiredBranch()
    {
        IString stringSwitch = new StringSwitch<IString>(
            new String("branch4"),
            [
                new KeyValuePair<IString, IString>(
                    new String("branch1"),
                    new String("value1")
                ),
                new KeyValuePair<IString, IString>(
                    new String("branch2"),
                    new String("value2")
                ),
                new KeyValuePair<IString, IString>(
                    new String("branch3"),
                    new String("value3")
                ),
                new KeyValuePair<IString, IString>(
                    new String("branch4"),
                    new String("value4")
                ),
            ],
            x => new DeterminedHash(x)
        );

        Assert.Equal("value4", stringSwitch.TextValue);
    }

    [Fact]
    public void GiveDefaultValueOnNotExistingSelector()
    {
        IString stringSwitch = new StringSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IString>(
                    new String("branch1"),
                    new String("value1")
                ),
                new KeyValuePair<IString, IString>(
                    new String("branch2"),
                    new String("value2")
                ),
                new KeyValuePair<IString, IString>(
                    new String("branch3"),
                    new String("value3")
                ),
                new KeyValuePair<IString, IString>(
                    new String("branch4"),
                    new String("value4")
                ),
            ],
            x => new DeterminedHash(x),
            new String("default")
        );

        Assert.Equal("default", stringSwitch.TextValue);
    }

    [Fact]
    public void ThrowsExceptionOnNotExistingSelectorWithNoDefaultValue()
    {
        IString stringSwitch = new StringSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IString>(
                    new String("branch1"),
                    new String("value1")
                ),
                new KeyValuePair<IString, IString>(
                    new String("branch2"),
                    new String("value2")
                ),
                new KeyValuePair<IString, IString>(
                    new String("branch3"),
                    new String("value3")
                ),
                new KeyValuePair<IString, IString>(
                    new String("branch4"),
                    new String("value4")
                ),
            ],
            x => new DeterminedHash(x)
        );

        _ = Assert.Throws<InvalidOperationException>(() => stringSwitch.TextValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new StringSwitch<IString>(
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
            new StringSwitch<IString>(
                new RandomString(),
                [],
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
