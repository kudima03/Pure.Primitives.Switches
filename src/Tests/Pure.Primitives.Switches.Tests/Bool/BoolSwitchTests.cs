using Pure.HashCodes;
using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Bool;
using Pure.Primitives.Random.String;
using Pure.Primitives.Switches.Bool;

namespace Pure.Primitives.Switches.Tests.Bool;

using String = String.String;

public sealed record BoolSwitchTests
{
    [Fact]
    public void FindRequiredBranch()
    {
        IBool boolSwitch = new BoolSwitch<IString>(
            new String("branch1"),
            [
                new KeyValuePair<IString, IBool>(new String("branch1"), new False()),
                new KeyValuePair<IString, IBool>(new String("branch2"), new False()),
            ],
            x => new DeterminedHash(x)
        );

        Assert.False(boolSwitch.BoolValue);
    }

    [Fact]
    public void GiveDefaultValueOnNotExistingSelector()
    {
        IBool boolSwitch = new BoolSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IBool>(new String("branch1"), new False()),
                new KeyValuePair<IString, IBool>(new String("branch2"), new False()),
            ],
            x => new DeterminedHash(x),
            new True()
        );

        Assert.True(boolSwitch.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnNotExistingSelectorWithNoDefaultValue()
    {
        IBool boolSwitch = new BoolSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IBool>(new String("branch1"), new False()),
                new KeyValuePair<IString, IBool>(new String("branch2"), new False()),
            ],
            x => new DeterminedHash(x)
        );

        _ = Assert.Throws<InvalidOperationException>(() => boolSwitch.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new BoolSwitch<IString>(
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
            new BoolSwitch<IString>(
                new RandomString(),
                [],
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
