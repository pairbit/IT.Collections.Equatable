using System;
using System.Collections.Generic;
using System.Linq;

namespace IT.Collections.Equatable;

public class EquatableSortedSet<T> : SortedSet<T>, IEquatable<EquatableSortedSet<T>>
{
    private readonly IEqualityComparer<T> _equalityComparer;

    public EquatableSortedSet(IComparer<T>? comparer = null, IEqualityComparer<T>? equalityComparer = null) : base(comparer)
    {
        _equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
    }

    public EquatableSortedSet(IEnumerable<T> collection, IComparer<T>? comparer = null, IEqualityComparer<T>? equalityComparer = null) : base(collection, comparer)
    {
        _equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
    }

    public IEqualityComparer<T> EqualityComparer => _equalityComparer;

    public override bool Equals(object? obj) => Equals(obj as EquatableSortedSet<T>);

    public bool Equals(EquatableSortedSet<T>? other) => ReferenceEquals(this, other) || other is not null && this.SequenceEqual(other, _equalityComparer);

    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var item in this)
        {
            hash.Add(item, _equalityComparer);
        }
        return hash.ToHashCode();
    }
}