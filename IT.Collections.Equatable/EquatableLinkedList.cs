using System;
using System.Collections.Generic;
using System.Linq;

namespace IT.Collections.Equatable;

public class EquatableLinkedList<T> : LinkedList<T>, IEquatable<EquatableLinkedList<T>>
{
    private readonly IEqualityComparer<T> _comparer;

    public EquatableLinkedList(IEqualityComparer<T>? comparer = null)
    {
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public EquatableLinkedList(IEnumerable<T> collection, IEqualityComparer<T>? comparer = null) : base(collection)
    {
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public IEqualityComparer<T> Comparer => _comparer;

    public override bool Equals(object? obj) => Equals(obj as EquatableLinkedList<T>);

    public bool Equals(EquatableLinkedList<T>? other) => ReferenceEquals(this, other) || other is not null && this.SequenceEqual(other, _comparer);

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