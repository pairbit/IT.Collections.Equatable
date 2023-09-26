#if NET6_0_OR_GREATER

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IT.Collections.Equatable;

public class EquatableReadOnlySet<T> : IReadOnlySet<T>, IEquatable<EquatableReadOnlySet<T>>
{
    private readonly IEqualityComparer<T> _comparer;
    private readonly IReadOnlySet<T> _set;

    public readonly static EquatableReadOnlySet<T> Empty = new(new HashSet<T>());

    public EquatableReadOnlySet(IReadOnlySet<T> set, IEqualityComparer<T>? comparer = null)
    {
        _set = set ?? throw new ArgumentNullException(nameof(set));
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public IEqualityComparer<T> Comparer => _comparer;

    public int Count => _set.Count;

    public bool Contains(T item) => _set.Contains(item);

    public IEnumerator<T> GetEnumerator() => _set.GetEnumerator();

    public bool IsProperSubsetOf(IEnumerable<T> other) => _set.IsProperSubsetOf(other);

    public bool IsProperSupersetOf(IEnumerable<T> other) => _set.IsProperSupersetOf(other);

    public bool IsSubsetOf(IEnumerable<T> other) => _set.IsSubsetOf(other);

    public bool IsSupersetOf(IEnumerable<T> other) => _set.IsSupersetOf(other);

    public bool Overlaps(IEnumerable<T> other) => _set.Overlaps(other);

    public bool SetEquals(IEnumerable<T> other) => _set.SetEquals(other);

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_set).GetEnumerator();

    public override bool Equals(object? obj) => Equals(obj as EquatableReadOnlySet<T>);

    public bool Equals(EquatableReadOnlySet<T>? other) => ReferenceEquals(this, other) || other is not null && this.SequenceEqual(other, _comparer);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var item in _set)
        {
            hash.Add(item, _comparer);
        }
        return hash.ToHashCode();
    }
}

#endif