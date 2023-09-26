using System;
using System.Collections.Generic;

namespace IT.Collections.Equatable;

public class EquatableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IEquatable<EquatableDictionary<TKey, TValue>>
    where TKey : notnull
{
    private readonly IEqualityComparer<TValue> _valueComparer;

    public EquatableDictionary(
        IEqualityComparer<TKey>? keyComparer,
        IEqualityComparer<TValue>? valueComparer = null) : base(keyComparer)
    {
        _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
    }

    public EquatableDictionary(int capacity,
        IEqualityComparer<TKey>? keyComparer,
        IEqualityComparer<TValue>? valueComparer = null) : base(capacity, keyComparer)
    {
        _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
    }

#if NETSTANDARD2_0

    public EquatableDictionary(
        IDictionary<TKey, TValue> dictionary,
        IEqualityComparer<TKey>? keyComparer,
        IEqualityComparer<TValue>? valueComparer = null) : base(dictionary, keyComparer)
    {
        _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
    }

#else

    public EquatableDictionary(
        IEnumerable<KeyValuePair<TKey, TValue>> collection,
        IEqualityComparer<TKey>? keyComparer,
        IEqualityComparer<TValue>? valueComparer = null) : base(collection, keyComparer)
    {
        _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
    }

#endif

    public IEqualityComparer<TValue> ValueComparer => _valueComparer;

    public override bool Equals(object? obj) => Equals(obj as EquatableDictionary<TKey, TValue>);

    public bool Equals(EquatableDictionary<TKey, TValue>? other)
        => ReferenceEquals(this, other) || other is not null &&
           Comparer == other.Comparer &&
           Count == other.Count &&
           _valueComparer == other._valueComparer && SequenceEqual(other);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var item in this)
        {
            hash.Add(item.Key, Comparer);
            hash.Add(item.Value, _valueComparer);
        }
        return hash.ToHashCode();
    }

    private bool SequenceEqual(EquatableDictionary<TKey, TValue> other)
    {
        using var e1 = GetEnumerator();
        using var e2 = other.GetEnumerator();

        while (e1.MoveNext())
        {
            if (!(e2.MoveNext()
                && Comparer.Equals(e1.Current.Key, e2.Current.Key)
                && _valueComparer.Equals(e1.Current.Value, e2.Current.Value)))
            {
                return false;
            }
        }

        return !e2.MoveNext();
    }
}