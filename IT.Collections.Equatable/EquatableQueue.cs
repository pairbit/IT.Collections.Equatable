using System;
using System.Collections.Generic;

namespace IT.Collections.Equatable;

using Internal;

public class EquatableQueue<T> : Queue<T>, IEquatable<EquatableQueue<T>>
{
    private readonly IEqualityComparer<T>? _comparer;

    public EquatableQueue(IEqualityComparer<T>? comparer = null)
    {
        if (comparer != null && comparer != EqualityComparer<T>.Default)
            _comparer = comparer;
    }

    public EquatableQueue(int capacity, IEqualityComparer<T>? comparer = null) : base(capacity)
    {
        if (comparer != null && comparer != EqualityComparer<T>.Default)
            _comparer = comparer;
    }

    public EquatableQueue(IEnumerable<T> collection, IEqualityComparer<T>? comparer = null) : base(collection)
    {
        if (comparer != null && comparer != EqualityComparer<T>.Default)
            _comparer = comparer;
    }

#if NET6_0_OR_GREATER
    public int Capacity
    {
        get => EnsureCapacity(0);
        set => EnsureCapacity(value);
    }
#endif

    public IEqualityComparer<T> Comparer => _comparer ?? EqualityComparer<T>.Default;

    public override bool Equals(object? obj) => Equals(obj as EquatableQueue<T>);

    public bool Equals(EquatableQueue<T>? other)
    {
        if (other == this) return true;
        if (other == null || other.Count != Count) return false;

        var comparer = _comparer;
        var otherComparer = other._comparer;

        return (comparer == otherComparer || (comparer != null && comparer.Equals(otherComparer))) &&
                SequenceEqual.Enumerable(this, other, comparer);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        var comparer = _comparer;
        hash.Add(comparer);
        foreach (var item in this)
        {
            hash.Add(item, comparer);
        }
        return hash.ToHashCode();
    }
}