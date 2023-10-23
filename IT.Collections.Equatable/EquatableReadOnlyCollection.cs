using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IT.Collections.Equatable;

internal class EquatableReadOnlyCollection<T> : IReadOnlyCollection<T>, IEquatable<EquatableReadOnlyCollection<T>>
{
    private readonly IEqualityComparer<T> _comparer;
    private readonly IReadOnlyCollection<T> _collection;

    public readonly static EquatableReadOnlyCollection<T> Empty = new(new LinkedList<T>());

    public EquatableReadOnlyCollection(IReadOnlyCollection<T> collection, IEqualityComparer<T>? comparer = null)
    {
        _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public IEqualityComparer<T> Comparer => _comparer;

    public int Count => _collection.Count;

    public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_collection).GetEnumerator();

    public override bool Equals(object? obj) => Equals(obj as EquatableReadOnlyCollection<T>);

    public bool Equals(EquatableReadOnlyCollection<T>? other) => ReferenceEquals(this, other) || other is not null && this.SequenceEqual(other, _comparer);

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