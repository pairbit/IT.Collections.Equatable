using System;
using System.Collections.Generic;
using System.Linq;

namespace IT.Collections.Equatable;

public class EquatableList<T> : List<T>, IEquatable<EquatableList<T>>
{
    private readonly IEqualityComparer<T> _comparer;

    public EquatableList(IEqualityComparer<T>? comparer = null)
    {
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public EquatableList(int capacity, IEqualityComparer<T>? comparer = null) : base(capacity)
    {
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public EquatableList(IEnumerable<T> collection, IEqualityComparer<T>? comparer = null) : base(collection)
    {
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public IEqualityComparer<T> Comparer => _comparer;

    public override bool Equals(object? obj) => Equals(obj as EquatableList<T>);

    public bool Equals(EquatableList<T>? other) => ReferenceEquals(this, other) || other is not null && this.SequenceEqual(other, _comparer);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        for (int i = 0; i < Count; i++)
        {
            hash.Add(this[i], _comparer);
        }
        return hash.ToHashCode();
    }
}