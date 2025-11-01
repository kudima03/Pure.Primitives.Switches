using Pure.HashCodes.Abstractions;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Abstractions.Time;

namespace Pure.Primitives.Switches.Time;

public sealed record TimeSwitch<TSelector> : ITime
{
    public TimeSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, ITime>> branches,
        Func<TSelector, IDeterminedHash> hashFactory
    )
        : this(parameter, branches, hashFactory, null!) { }

    public TimeSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, ITime>> branches,
        Func<TSelector, IDeterminedHash> hashFactory,
        ITime defaultValue
    )
        : this(
            new Lazy<ITime>(() =>
            {
                IEnumerable<byte> parameterHash = hashFactory(parameter);

                IEnumerable<ITime> filteredBranches = branches
                    .Where(x => parameterHash.SequenceEqual(hashFactory(x.Key)))
                    .Select(x => x.Value);

                return defaultValue == null
                    ? filteredBranches.First()
                    : filteredBranches.FirstOrDefault(defaultValue);
            })
        )
    { }

    private TimeSwitch(Lazy<ITime> cachedTime)
    {
        CachedTime = cachedTime;
    }

    private Lazy<ITime> CachedTime { get; }

    public INumber<ushort> Hour => CachedTime.Value.Hour;

    public INumber<ushort> Minute => CachedTime.Value.Minute;

    public INumber<ushort> Second => CachedTime.Value.Second;

    public INumber<ushort> Millisecond => CachedTime.Value.Millisecond;

    public INumber<ushort> Microsecond => CachedTime.Value.Microsecond;

    public INumber<ushort> Nanosecond => CachedTime.Value.Nanosecond;

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
