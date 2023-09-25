using System;
using System.Collections.Generic;
using System.Linq;

namespace IT.Collections.Equatable;

public class EquatableHashSet<T> : HashSet<T>, IEquatable<EquatableHashSet<T>>
{
    public EquatableHashSet(IEqualityComparer<T>? comparer = null) : base(comparer)
    {
    }

    public EquatableHashSet(int capacity, IEqualityComparer<T>? comparer = null) : base(capacity, comparer)
    {
    }

    public EquatableHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer = null) : base(collection, comparer)
    {
    }

    public override bool Equals(object? obj) => Equals(obj as EquatableHashSet<T>);

    public bool Equals(EquatableHashSet<T>? other) => ReferenceEquals(this, other) || other is not null && this.SequenceEqual(other, Comparer);

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