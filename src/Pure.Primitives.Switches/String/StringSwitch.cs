using System.Collections;
using Pure.HashCodes.Abstractions;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;

namespace Pure.Primitives.Switches.String;

using Char = Primitives.Char.Char;

public sealed record StringSwitch<TSelector> : IString
{
    private readonly TSelector _parameter;

    private readonly Func<TSelector, IDeterminedHash> _hashFactory;

    private readonly IString? _defaultValue;

    private readonly IEnumerable<KeyValuePair<TSelector, IString>> _branches;

    public StringSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IString>> branches,
        Func<TSelector, IDeterminedHash> hashFactory
    )
        : this(parameter, branches, hashFactory, null!) { }

    public StringSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IString>> branches,
        Func<TSelector, IDeterminedHash> hashFactory,
        IString defaultValue
    )
    {
        _parameter = parameter;
        _hashFactory = hashFactory;
        _defaultValue = defaultValue;
        _branches = branches;
    }

    public string TextValue
    {
        get
        {
            IEnumerable<byte> parameterHash = _hashFactory(_parameter);

            IEnumerable<IString> filteredBranches = _branches
                .Where(x => parameterHash.SequenceEqual(_hashFactory(x.Key)))
                .Select(x => x.Value);

            return _defaultValue == null
                ? filteredBranches.First().TextValue
                : filteredBranches.FirstOrDefault(_defaultValue).TextValue;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<IChar> GetEnumerator()
    {
        return TextValue.Select(x => new Char(x)).GetEnumerator();
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
