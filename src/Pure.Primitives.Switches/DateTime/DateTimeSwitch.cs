using Pure.HashCodes.Abstractions;
using Pure.Primitives.Abstractions.DateTime;
using Pure.Primitives.Abstractions.Number;

namespace Pure.Primitives.Switches.DateTime;

public sealed record DateTimeSwitch<TSelector> : IDateTime
{
    public DateTimeSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IDateTime>> branches,
        Func<TSelector, IDeterminedHash> hashFactory
    )
        : this(parameter, branches, hashFactory, null!) { }

    public DateTimeSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IDateTime>> branches,
        Func<TSelector, IDeterminedHash> hashFactory,
        IDateTime defaultValue
    )
        : this(
            new Lazy<IDateTime>(() =>
            {
                IEnumerable<byte> parameterHash = hashFactory(parameter);

                IEnumerable<IDateTime> filteredBranches = branches
                    .Where(x => parameterHash.SequenceEqual(hashFactory(x.Key)))
                    .Select(x => x.Value);

                return defaultValue == null
                    ? filteredBranches.First()
                    : filteredBranches.FirstOrDefault(defaultValue);
            })
        )
    { }

    private DateTimeSwitch(Lazy<IDateTime> cachedDateTime)
    {
        CachedDateTime = cachedDateTime;
    }

    private Lazy<IDateTime> CachedDateTime { get; }

    public INumber<ushort> Year => CachedDateTime.Value.Year;

    public INumber<ushort> Month => CachedDateTime.Value.Month;

    public INumber<ushort> Day => CachedDateTime.Value.Day;

    public INumber<ushort> Hour => CachedDateTime.Value.Hour;

    public INumber<ushort> Minute => CachedDateTime.Value.Minute;

    public INumber<ushort> Second => CachedDateTime.Value.Second;

    public INumber<ushort> Millisecond => CachedDateTime.Value.Millisecond;

    public INumber<ushort> Microsecond => CachedDateTime.Value.Microsecond;

    public INumber<ushort> Nanosecond => CachedDateTime.Value.Nanosecond;

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
