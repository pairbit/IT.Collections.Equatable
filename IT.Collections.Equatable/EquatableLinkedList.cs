﻿using System;
using System.Collections.Generic;

namespace IT.Collections.Equatable;

using Internal;

public class EquatableLinkedList<T> : LinkedList<T>, IEquatable<EquatableLinkedList<T>>
{
    private readonly IEqualityComparer<T>? _comparer;

    public EquatableLinkedList(IEqualityComparer<T>? comparer = null)
    {
        if (comparer != null && comparer != EqualityComparer<T>.Default)
            _comparer = comparer;
    }

    public EquatableLinkedList(IEnumerable<T> collection, IEqualityComparer<T>? comparer = null) : base(collection)
    {
        if (comparer != null && comparer != EqualityComparer<T>.Default)
            _comparer = comparer;
    }

    public IEqualityComparer<T> Comparer => _comparer ?? EqualityComparer<T>.Default;

    public override bool Equals(object? other) => Equals(other as EquatableLinkedList<T>);

    public bool Equals(EquatableLinkedList<T>? other)
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