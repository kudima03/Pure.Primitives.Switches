using Pure.HashCodes.Abstractions;
using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Abstractions.Number;

namespace Pure.Primitives.Switches.Date;

public sealed record DateSwitch<TSelector> : IDate
{
    public DateSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IDate>> branches,
        Func<TSelector, IDeterminedHash> hashFactory
    )
        : this(parameter, branches, hashFactory, null!) { }

    public DateSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IDate>> branches,
        Func<TSelector, IDeterminedHash> hashFactory,
        IDate defaultValue
    )
        : this(
            new Lazy<IDate>(() =>
            {
                IEnumerable<byte> parameterHash = hashFactory(parameter);

                IEnumerable<IDate> filteredBranches = branches
                    .Where(x => parameterHash.SequenceEqual(hashFactory(x.Key)))
                    .Select(x => x.Value);

                return defaultValue == null
                    ? filteredBranches.First()
                    : filteredBranches.FirstOrDefault(defaultValue);
            })
        )
    { }

    private DateSwitch(Lazy<IDate> cachedTime)
    {
        CachedTime = cachedTime;
    }

    private Lazy<IDate> CachedTime { get; }

    public INumber<ushort> Day => CachedTime.Value.Day;

    public INumber<ushort> Month => CachedTime.Value.Month;

    public INumber<ushort> Year => CachedTime.Value.Year;

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
