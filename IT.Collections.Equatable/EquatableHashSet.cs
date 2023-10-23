using System;
using System.Collections.Generic;

namespace IT.Collections.Equatable;

using Internal;

public class EquatableHashSet<T> : HashSet<T>, IEquatable<EquatableHashSet<T>>
{
    public EquatableHashSet(IEqualityComparer<T>? comparer = null) : base(comparer)
    {
    }

#if !NET461 && !NETSTANDARD2_0

    public EquatableHashSet(int capacity, IEqualityComparer<T>? comparer = null) : base(capacity, comparer)
    {
    }

#endif

    public EquatableHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer = null) : base(collection, comparer)
    {
    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
    public int Capacity
    {
        get => EnsureCapacity(0);
        set => EnsureCapacity(value);
    }
#endif

    public override bool Equals(object? other) => Equals(other as EquatableHashSet<T>);

    public bool Equals(EquatableHashSet<T>? other)
    {
        if (other == this) return true;
        if (other == null || other.Count != Count) return false;

        var comparer = Comparer;
        var otherComparer = other.Comparer;
        
        return (comparer == otherComparer || comparer.Equals(otherComparer)) &&
                SequenceEqual.EnumerableRequired(this, other, comparer);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        var comparer = Comparer;
        foreach (var item in this)
        {
            hash.Add(item, comparer);
        }
        return hash.ToHashCode();
    }
}