using Pure.HashCodes;
using Pure.Primitives.Abstractions.DayOfWeek;
using Pure.Primitives.Abstractions.Number;

namespace Pure.Primitives.Switches.DayOfWeek;

public sealed record DayOfWeekSwitch<TSelector> : IDayOfWeek
{
    private readonly TSelector _parameter;

    private readonly Func<TSelector, IDeterminedHash> _hashFactory;

    private readonly IDayOfWeek? _defaultValue;

    private readonly IEnumerable<KeyValuePair<TSelector, IDayOfWeek>> _branches;

    public DayOfWeekSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IDayOfWeek>> branches,
        Func<TSelector, IDeterminedHash> hashFactory
    )
        : this(parameter, branches, hashFactory, null!) { }

    public DayOfWeekSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IDayOfWeek>> branches,
        Func<TSelector, IDeterminedHash> hashFactory,
        IDayOfWeek defaultValue
    )
    {
        _parameter = parameter;
        _hashFactory = hashFactory;
        _defaultValue = defaultValue;
        _branches = branches;
    }

    public INumber<ushort> DayNumberValue
    {
        get
        {
            IEnumerable<byte> parameterHash = _hashFactory(_parameter);

            IEnumerable<IDayOfWeek> filteredBranches = _branches
                .Where(x => parameterHash.SequenceEqual(_hashFactory(x.Key)))
                .Select(x => x.Value);

            return _defaultValue == null
                ? filteredBranches.First().DayNumberValue
                : filteredBranches.FirstOrDefault(_defaultValue).DayNumberValue;
        }
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
