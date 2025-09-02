using Pure.HashCodes;
using Pure.Primitives.Abstractions.DayOfWeek;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.DayOfWeek;
using Pure.Primitives.Random.String;
using Pure.Primitives.Switches.DayOfWeek;

namespace Pure.Primitives.Switches.Tests.DayOfWeek;

using String = Primitives.String.String;

public sealed record DayOfWeekSwitchTests
{
    [Fact]
    public void FindRequiredBranch()
    {
        IDayOfWeek dayOfWeekSwitch = new DayOfWeekSwitch<IString>(
            new String("branch4"),
            [
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch1"),
                    new Monday()
                ),
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch2"),
                    new Tuesday()
                ),
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch3"),
                    new Wednesday()
                ),
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch4"),
                    new Thursday()
                ),
            ],
            x => new DeterminedHash(x)
        );

        Assert.Equal(4, dayOfWeekSwitch.DayNumberValue.NumberValue);
    }

    [Fact]
    public void GiveDefaultValueOnNotExistingSelector()
    {
        IDayOfWeek dayOfWeekSwitch = new DayOfWeekSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch1"),
                    new Monday()
                ),
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch2"),
                    new Tuesday()
                ),
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch3"),
                    new Wednesday()
                ),
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch4"),
                    new Thursday()
                ),
            ],
            x => new DeterminedHash(x),
            new Friday()
        );

        Assert.Equal(5, dayOfWeekSwitch.DayNumberValue.NumberValue);
    }

    [Fact]
    public void ThrowsExceptionOnNotExistingSelectorWithNoDefaultValue()
    {
        IDayOfWeek dayOfWeekSwitch = new DayOfWeekSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch1"),
                    new Monday()
                ),
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch2"),
                    new Tuesday()
                ),
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch3"),
                    new Wednesday()
                ),
                new KeyValuePair<IString, IDayOfWeek>(
                    new String("branch4"),
                    new Thursday()
                ),
            ],
            x => new DeterminedHash(x)
        );

        _ = Assert.Throws<InvalidOperationException>(() =>
            dayOfWeekSwitch.DayNumberValue
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new DayOfWeekSwitch<IString>(
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
            new DayOfWeekSwitch<IString>(
                new RandomString(),
                [],
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
