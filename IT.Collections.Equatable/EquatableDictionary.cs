using System;
using System.Collections.Generic;

namespace IT.Collections.Equatable;

using Internal;

public class EquatableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IEquatable<EquatableDictionary<TKey, TValue>>
    where TKey : notnull
{
    private readonly IEqualityComparer<TValue>? _valueComparer;

    public EquatableDictionary(
        IEqualityComparer<TKey>? keyComparer,
        IEqualityComparer<TValue>? valueComparer = null) : base(keyComparer)
    {
        if (valueComparer != null && valueComparer != EqualityComparer<TValue>.Default)
            _valueComparer = valueComparer;
    }

    public EquatableDictionary(int capacity,
        IEqualityComparer<TKey>? keyComparer,
        IEqualityComparer<TValue>? valueComparer = null) : base(capacity, keyComparer)
    {
        if (valueComparer != null && valueComparer != EqualityComparer<TValue>.Default)
            _valueComparer = valueComparer;
    }

#if NETSTANDARD2_0 || NET461_OR_GREATER

    public EquatableDictionary(
        IDictionary<TKey, TValue> dictionary,
        IEqualityComparer<TKey>? keyComparer,
        IEqualityComparer<TValue>? valueComparer = null) : base(dictionary, keyComparer)
    {
        if (valueComparer != null && valueComparer != EqualityComparer<TValue>.Default)
            _valueComparer = valueComparer;
    }

#else

    public EquatableDictionary(
        IEnumerable<KeyValuePair<TKey, TValue>> collection,
        IEqualityComparer<TKey>? keyComparer,
        IEqualityComparer<TValue>? valueComparer = null) : base(collection, keyComparer)
    {
        if (valueComparer != null && valueComparer != EqualityComparer<TValue>.Default)
            _valueComparer = valueComparer;
    }

#endif

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
    public int Capacity
    {
        get => EnsureCapacity(0);
        set => EnsureCapacity(value);
    }
#endif

    public IEqualityComparer<TValue> ValueEqualityComparer => _valueComparer ?? EqualityComparer<TValue>.Default;

    public override bool Equals(object? other) => Equals(other as EquatableDictionary<TKey, TValue>);

    public bool Equals(EquatableDictionary<TKey, TValue>? other)
    {
        if (other == this) return true;
        if (other == null || other.Count != Count) return false;

        var keyComparer = Comparer;
        var otherKeyComparer = other.Comparer;
        if (keyComparer != otherKeyComparer && !keyComparer.Equals(otherKeyComparer)) return false;

        var valueComparer = _valueComparer;
        var otherValueComparer = other._valueComparer;

        return (valueComparer == otherValueComparer || (valueComparer != null && valueComparer.Equals(otherValueComparer))) &&
                SequenceEqual.Enumerable(this, other, keyComparer, valueComparer);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        var keyComparer = Comparer;
        var valueComparer = _valueComparer;
        foreach (var item in this)
        {
            hash.Add(item.Key, keyComparer);
            hash.Add(item.Value, valueComparer);
        }
        return hash.ToHashCode();
    }
}