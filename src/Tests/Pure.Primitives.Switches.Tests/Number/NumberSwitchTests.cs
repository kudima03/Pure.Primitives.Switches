using Pure.HashCodes;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Number;
using Pure.Primitives.Random.String;
using Pure.Primitives.Switches.Number;

namespace Pure.Primitives.Switches.Tests.Number;

using String = Primitives.String.String;

public sealed record NumberSwitchTests
{
    [Fact]
    public void FindRequiredBranch()
    {
        INumber<int> numberSwitch = new NumberSwitch<IString, int>(
            new String("branch4"),
            [
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch1"),
                    new Int(1)
                ),
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch2"),
                    new Int(2)
                ),
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch3"),
                    new Int(3)
                ),
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch4"),
                    new Int(4)
                ),
            ],
            x => new DeterminedHash(x)
        );

        Assert.Equal(4, numberSwitch.NumberValue);
    }

    [Fact]
    public void GiveDefaultValueOnNotExistingSelector()
    {
        INumber<int> numberSwitch = new NumberSwitch<IString, int>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch1"),
                    new Int(1)
                ),
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch2"),
                    new Int(2)
                ),
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch3"),
                    new Int(3)
                ),
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch4"),
                    new Int(4)
                ),
            ],
            x => new DeterminedHash(x),
            new Int(5)
        );

        Assert.Equal(5, numberSwitch.NumberValue);
    }

    [Fact]
    public void ThrowsExceptionOnNotExistingSelectorWithNoDefaultValue()
    {
        INumber<int> numberSwitch = new NumberSwitch<IString, int>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch1"),
                    new Int(1)
                ),
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch2"),
                    new Int(2)
                ),
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch3"),
                    new Int(3)
                ),
                new KeyValuePair<IString, INumber<int>>(
                    new String("branch4"),
                    new Int(4)
                ),
            ],
            x => new DeterminedHash(x)
        );

        _ = Assert.Throws<InvalidOperationException>(() => numberSwitch.NumberValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new NumberSwitch<IString, int>(
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
            new NumberSwitch<IString, int>(
                new RandomString(),
                [],
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
