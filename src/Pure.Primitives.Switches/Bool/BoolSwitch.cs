using Pure.HashCodes;
using Pure.Primitives.Abstractions.Bool;

namespace Pure.Primitives.Switches.Bool;

public sealed record BoolSwitch<TSelector> : IBool
{
    private readonly TSelector _parameter;

    private readonly Func<TSelector, IDeterminedHash> _hashFactory;

    private readonly IBool? _defaultValue;

    private readonly IEnumerable<KeyValuePair<TSelector, IBool>> _branches;

    public BoolSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IBool>> branches,
        Func<TSelector, IDeterminedHash> hashFactory
    )
        : this(parameter, branches, hashFactory, null!) { }

    public BoolSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IBool>> branches,
        Func<TSelector, IDeterminedHash> hashFactory,
        IBool defaultValue
    )
    {
        _parameter = parameter;
        _hashFactory = hashFactory;
        _defaultValue = defaultValue;
        _branches = branches;
    }

    public bool BoolValue
    {
        get
        {
            IEnumerable<byte> parameterHash = _hashFactory(_parameter);

            IEnumerable<IBool> filteredBranches = _branches
                .Where(x => parameterHash.SequenceEqual(_hashFactory(x.Key)))
                .Select(x => x.Value);

            return _defaultValue == null
                ? filteredBranches.First().BoolValue
                : filteredBranches.FirstOrDefault(_defaultValue).BoolValue;
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
