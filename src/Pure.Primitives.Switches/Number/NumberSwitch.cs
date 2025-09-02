using Pure.HashCodes;
using Pure.Primitives.Abstractions.Number;

namespace Pure.Primitives.Switches.Number;

public sealed record NumberSwitch<TSelector, TNumber> : INumber<TNumber>
    where TNumber : System.Numerics.INumber<TNumber>
{
    private readonly TSelector _parameter;

    private readonly Func<TSelector, IDeterminedHash> _hashFactory;

    private readonly INumber<TNumber>? _defaultValue;

    private readonly IEnumerable<KeyValuePair<TSelector, INumber<TNumber>>> _branches;

    public NumberSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, INumber<TNumber>>> branches,
        Func<TSelector, IDeterminedHash> hashFactory
    )
        : this(parameter, branches, hashFactory, null!) { }

    public NumberSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, INumber<TNumber>>> branches,
        Func<TSelector, IDeterminedHash> hashFactory,
        INumber<TNumber> defaultValue
    )
    {
        _parameter = parameter;
        _hashFactory = hashFactory;
        _defaultValue = defaultValue;
        _branches = branches;
    }

    public TNumber NumberValue
    {
        get
        {
            IEnumerable<byte> parameterHash = _hashFactory(_parameter);

            IEnumerable<INumber<TNumber>> filteredBranches = _branches
                .Where(x => parameterHash.SequenceEqual(_hashFactory(x.Key)))
                .Select(x => x.Value);

            return _defaultValue == null
                ? filteredBranches.First().NumberValue
                : filteredBranches.FirstOrDefault(_defaultValue).NumberValue;
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
