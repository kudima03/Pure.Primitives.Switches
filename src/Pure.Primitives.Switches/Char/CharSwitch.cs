using Pure.HashCodes;
using Pure.Primitives.Abstractions.Char;

namespace Pure.Primitives.Switches.Char;

public sealed record CharSwitch<TSelector> : IChar
{
    private readonly TSelector _parameter;

    private readonly Func<TSelector, IDeterminedHash> _hashFactory;

    private readonly IChar? _defaultValue;

    private readonly IEnumerable<KeyValuePair<TSelector, IChar>> _branches;

    public CharSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IChar>> branches,
        Func<TSelector, IDeterminedHash> hashFactory
    )
        : this(parameter, branches, hashFactory, null!) { }

    public CharSwitch(
        TSelector parameter,
        IEnumerable<KeyValuePair<TSelector, IChar>> branches,
        Func<TSelector, IDeterminedHash> hashFactory,
        IChar defaultValue
    )
    {
        _parameter = parameter;
        _hashFactory = hashFactory;
        _defaultValue = defaultValue;
        _branches = branches;
    }

    public char CharValue
    {
        get
        {
            IEnumerable<byte> parameterHash = _hashFactory(_parameter);

            IEnumerable<IChar> filteredBranches = _branches
                .Where(x => parameterHash.SequenceEqual(_hashFactory(x.Key)))
                .Select(x => x.Value);

            return _defaultValue == null
                ? filteredBranches.First().CharValue
                : filteredBranches.FirstOrDefault(_defaultValue).CharValue;
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
