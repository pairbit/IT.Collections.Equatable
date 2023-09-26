using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IT.Collections.Equatable;

public class EquatableReadOnlyDictionary<TKey, TValue> : ReadOnlyDictionary<TKey, TValue>, IEquatable<EquatableReadOnlyDictionary<TKey, TValue>>
    where TKey : notnull
{
    private readonly IEqualityComparer<TKey> _keyComparer;
    private readonly IEqualityComparer<TValue> _valueComparer;

    public EquatableReadOnlyDictionary
        (IDictionary<TKey, TValue> dictionary,
        IEqualityComparer<TKey>? keyComparer = null,
        IEqualityComparer<TValue>? valueComparer = null) : base(dictionary)
    {
        _keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
        _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
    }

    public IEqualityComparer<TKey> KeyComparer => _keyComparer;

    public IEqualityComparer<TValue> ValueComparer => _valueComparer;

    public override bool Equals(object? obj) => Equals(obj as EquatableReadOnlyDictionary<TKey, TValue>);

    public bool Equals(EquatableReadOnlyDictionary<TKey, TValue>? other)
        => ReferenceEquals(this, other) || other is not null &&
           Count == other.Count &&
           _keyComparer == other._keyComparer &&
           _valueComparer == other._valueComparer && SequenceEqual(other);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var item in this)
        {
            hash.Add(item.Key, _keyComparer);
            hash.Add(item.Value, _valueComparer);
        }
        return hash.ToHashCode();
    }

    private bool SequenceEqual(EquatableReadOnlyDictionary<TKey, TValue> other)
    {
        using var e1 = GetEnumerator();
        using var e2 = other.GetEnumerator();

        while (e1.MoveNext())
        {
            if (!(e2.MoveNext()
                && _keyComparer.Equals(e1.Current.Key, e2.Current.Key)
                && _valueComparer.Equals(e1.Current.Value, e2.Current.Value)))
            {
                return false;
            }
        }

        return !e2.MoveNext();
    }
}