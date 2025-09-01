using System.Globalization;
using Pure.HashCodes;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Pure.Primitives.Switches.Guid;

namespace Pure.Primitives.Switches.Tests.Guid;

using Guid = Primitives.Guid.Guid;
using String = String.String;

public sealed record GuidSwitchTests
{
    [Fact]
    public void FindRequiredBranch()
    {
        IGuid guidSwitch = new GuidSwitch<IString>(
            new String("branch4"),
            [
                new KeyValuePair<IString, IGuid>(
                    new String("branch1"),
                    new Guid(new System.Guid("A9AA2526-5119-455A-9EEC-4A17EE53F668"))
                ),
                new KeyValuePair<IString, IGuid>(
                    new String("branch2"),
                    new Guid(new System.Guid("D4A432E8-EBC1-4492-A04E-00E77DDC7C75"))
                ),
                new KeyValuePair<IString, IGuid>(
                    new String("branch3"),
                    new Guid(new System.Guid("1BFCE17A-BD87-46FE-9692-E8A527F186F1"))
                ),
                new KeyValuePair<IString, IGuid>(
                    new String("branch4"),
                    new Guid(new System.Guid("683724FD-24F9-4637-8B7E-9FDD1F986186"))
                ),
            ],
            x => new DeterminedHash(x)
        );

        Assert.Equal(
            "683724FD-24F9-4637-8B7E-9FDD1F986186",
            guidSwitch.GuidValue.ToString().ToUpper(CultureInfo.InvariantCulture)
        );
    }

    [Fact]
    public void GiveDefaultValueOnNotExistingSelector()
    {
        IGuid guidSwitch = new GuidSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IGuid>(
                    new String("branch1"),
                    new Guid(new System.Guid("A9AA2526-5119-455A-9EEC-4A17EE53F668"))
                ),
                new KeyValuePair<IString, IGuid>(
                    new String("branch2"),
                    new Guid(new System.Guid("D4A432E8-EBC1-4492-A04E-00E77DDC7C75"))
                ),
                new KeyValuePair<IString, IGuid>(
                    new String("branch3"),
                    new Guid(new System.Guid("1BFCE17A-BD87-46FE-9692-E8A527F186F1"))
                ),
                new KeyValuePair<IString, IGuid>(
                    new String("branch4"),
                    new Guid(new System.Guid("683724FD-24F9-4637-8B7E-9FDD1F986186"))
                ),
            ],
            x => new DeterminedHash(x),
            new Guid(new System.Guid("5D7D833C-6697-4060-B797-8FA5108840E1"))
        );

        Assert.Equal(
            "5D7D833C-6697-4060-B797-8FA5108840E1",
            guidSwitch.GuidValue.ToString().ToUpper(CultureInfo.InvariantCulture)
        );
    }

    [Fact]
    public void ThrowsExceptionOnNotExistingSelectorWithNoDefaultValue()
    {
        IGuid guidSwitch = new GuidSwitch<IString>(
            new String("asdasd"),
            [
                new KeyValuePair<IString, IGuid>(
                    new String("branch1"),
                    new Guid(new System.Guid("A9AA2526-5119-455A-9EEC-4A17EE53F668"))
                ),
                new KeyValuePair<IString, IGuid>(
                    new String("branch2"),
                    new Guid(new System.Guid("D4A432E8-EBC1-4492-A04E-00E77DDC7C75"))
                ),
                new KeyValuePair<IString, IGuid>(
                    new String("branch3"),
                    new Guid(new System.Guid("1BFCE17A-BD87-46FE-9692-E8A527F186F1"))
                ),
                new KeyValuePair<IString, IGuid>(
                    new String("branch4"),
                    new Guid(new System.Guid("683724FD-24F9-4637-8B7E-9FDD1F986186"))
                ),
            ],
            x => new DeterminedHash(x)
        );

        _ = Assert.Throws<InvalidOperationException>(() => guidSwitch.GuidValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new GuidSwitch<IString>(
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
            new GuidSwitch<IString>(
                new RandomString(),
                [],
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
