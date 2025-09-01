using Pure.HashCodes;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Pure.Primitives.Switches.Char;

namespace Pure.Primitives.Switches.Tests.Char;

using Char = Primitives.Char.Char;
using String = String.String;

public sealed record CharSwitchTests
{
    [Fact]
    public void FindRequiredBranch()
    {
        IChar charSwitch = new CharSwitch<IString>(
            new String("branch4"),
            [
                new KeyValuePair<IString, IChar>(new String("branch1"), new Char('Y')),
                new KeyValuePair<IString, IChar>(new String("branch2"), new Char('N')),
                new KeyValuePair<IString, IChar>(new String("branch3"), new Char('A')),
                new KeyValuePair<IString, IChar>(new String("branch4"), new Char('C')),
            ],
            x => new DeterminedHash(x)
        );

        Assert.Equal('C', charSwitch.CharValue);
    }

    [Fact]
    public void GiveDefaultValueOnNotExistingSelector()
    {
        IChar charSwitch = new CharSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IChar>(new String("branch1"), new Char('Y')),
                new KeyValuePair<IString, IChar>(new String("branch2"), new Char('N')),
                new KeyValuePair<IString, IChar>(new String("branch3"), new Char('A')),
                new KeyValuePair<IString, IChar>(new String("branch4"), new Char('C')),
            ],
            x => new DeterminedHash(x),
            new Char('V')
        );

        Assert.Equal('V', charSwitch.CharValue);
    }

    [Fact]
    public void ThrowsExceptionOnNotExistingSelectorWithNoDefaultValue()
    {
        IChar charSwitch = new CharSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IChar>(new String("branch1"), new Char('Y')),
                new KeyValuePair<IString, IChar>(new String("branch2"), new Char('N')),
                new KeyValuePair<IString, IChar>(new String("branch3"), new Char('A')),
                new KeyValuePair<IString, IChar>(new String("branch4"), new Char('C')),
            ],
            x => new DeterminedHash(x)
        );

        _ = Assert.Throws<InvalidOperationException>(() => charSwitch.CharValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new CharSwitch<IString>(
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
            new CharSwitch<IString>(
                new RandomString(),
                [],
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
