using Pure.HashCodes.Abstractions;
using Pure.Primitives.Abstractions.Guid;

namespace Pure.Primitives.Switches.Guid;

public sealed record GuidSwitch<TSelector> : IGuid
{
    private readonly TSelector _parameter;

    private readonly Func<TSelector, IDeterminedHash> _hashFactory;

    private readonly IGuid? _defaultValue;

    private readonly IEnumerable<KeyValuePair<TSelector, IGuid>> _branches;

    public GuidSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IGuid>> branches,
        Func<TSelector, IDeterminedHash> hashFactory
    )
        : this(parameter, branches, hashFactory, null!) { }

    public GuidSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IGuid>> branches,
        Func<TSelector, IDeterminedHash> hashFactory,
        IGuid defaultValue
    )
    {
        _parameter = parameter;
        _hashFactory = hashFactory;
        _defaultValue = defaultValue;
        _branches = branches;
    }

    public System.Guid GuidValue
    {
        get
        {
            IEnumerable<byte> parameterHash = _hashFactory(_parameter);

            IEnumerable<IGuid> filteredBranches = _branches
                .Where(x => parameterHash.SequenceEqual(_hashFactory(x.Key)))
                .Select(x => x.Value);

            return _defaultValue == null
                ? filteredBranches.First().GuidValue
                : filteredBranches.FirstOrDefault(_defaultValue).GuidValue;
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
