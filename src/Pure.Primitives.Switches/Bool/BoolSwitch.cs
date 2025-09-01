using Pure.HashCodes;
using Pure.Primitives.Abstractions.Bool;

namespace Pure.Primitives.Switches.Bool;

public sealed record BoolSwitch<TSelector> : IBool
{
    private readonly TSelector _parameter;

    private readonly Func<TSelector, IDeterminedHash> _hashFactory;

    private readonly IEnumerable<KeyValuePair<TSelector, IBool>> _branches;

    public BoolSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IBool>> branches,
        Func<TSelector, IDeterminedHash> hashFactory
    )
    {
        _parameter = parameter;
        _hashFactory = hashFactory;
        _branches = branches;
    }

    public bool BoolValue
    {
        get
        {
            IEnumerable<byte> parameterHash = _hashFactory(_parameter);
            return _branches
                .First(x => parameterHash.SequenceEqual(_hashFactory(x.Key)))
                .Value.BoolValue;
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
