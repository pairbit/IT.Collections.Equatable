using System;
using System.Collections.Generic;
using System.Linq;

namespace IT.Collections.Equatable;

public class EquatableQueue<T> : Queue<T>, IEquatable<EquatableQueue<T>>
{
    private readonly IEqualityComparer<T> _comparer;

    public EquatableQueue(IEqualityComparer<T>? comparer = null)
    {
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public EquatableQueue(int capacity, IEqualityComparer<T>? comparer = null) : base(capacity)
    {
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public EquatableQueue(IEnumerable<T> collection, IEqualityComparer<T>? comparer = null) : base(collection)
    {
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public IEqualityComparer<T> Comparer => _comparer;

    public override bool Equals(object? obj) => Equals(obj as EquatableQueue<T>);

    public bool Equals(EquatableQueue<T>? other) => ReferenceEquals(this, other) || other is not null && this.SequenceEqual(other, _comparer);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var item in this)
        {
            hash.Add(item, _comparer);
        }
        return hash.ToHashCode();
    }
}