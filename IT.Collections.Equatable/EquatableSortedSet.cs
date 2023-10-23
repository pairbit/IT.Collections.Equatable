using System;
using System.Collections.Generic;

namespace IT.Collections.Equatable;

using Internal;

public class EquatableSortedSet<T> : SortedSet<T>, IEquatable<EquatableSortedSet<T>>
{
    public EquatableSortedSet(IComparer<T>? comparer = null) : base(comparer)
    {
    }

    public EquatableSortedSet(IEnumerable<T> collection, IComparer<T>? comparer = null) : base(collection, comparer)
    {
    }

    public override bool Equals(object? other) => Equals(other as EquatableSortedSet<T>);

    public bool Equals(EquatableSortedSet<T>? other)
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
        hash.Add(Comparer);
        //TODO: GetHashCode ????
        foreach (var item in this)
        {
            hash.Add(item);
        }
        return hash.ToHashCode();
    }
}