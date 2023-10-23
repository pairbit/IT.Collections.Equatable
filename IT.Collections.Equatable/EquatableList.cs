using System;
using System.Collections.Generic;

namespace IT.Collections.Equatable;

using Internal;

public class EquatableList<T> : List<T>, IEquatable<EquatableList<T>>
{
    private readonly IEqualityComparer<T>? _comparer;

    public EquatableList(IEqualityComparer<T>? comparer = null)
    {
        if (comparer != null && comparer != EqualityComparer<T>.Default)
            _comparer = comparer;
    }

    public EquatableList(int capacity, IEqualityComparer<T>? comparer = null) : base(capacity)
    {
        if (comparer != null && comparer != EqualityComparer<T>.Default)
            _comparer = comparer;
    }

    public EquatableList(IEnumerable<T> collection, IEqualityComparer<T>? comparer = null) : base(collection)
    {
        if (comparer != null && comparer != EqualityComparer<T>.Default)
            _comparer = comparer;
    }

    public IEqualityComparer<T>? Comparer => _comparer ?? EqualityComparer<T>.Default;

    public override bool Equals(object? other) => Equals(other as EquatableList<T>);

    public bool Equals(EquatableList<T>? other)
    {
        if (other == this) return true;
        if (other == null || other.Count != Count) return false;

        var comparer = _comparer;
        var otherComparer = other._comparer;

        return (comparer == otherComparer || (comparer != null && comparer.Equals(otherComparer))) &&
                SequenceEqual.List(this, other, comparer);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        var comparer = _comparer;
        hash.Add(comparer);
        for (int i = 0; i < Count; i++)
        {
            hash.Add(this[i], comparer);
        }
        return hash.ToHashCode();
    }
}