using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IT.Collections.Equatable;

public class EquatableReadOnlyList<T> : IReadOnlyList<T>, IEquatable<EquatableReadOnlyList<T>>
{
    private readonly IEqualityComparer<T> _comparer;
    private readonly IReadOnlyList<T> _list;

    public readonly static EquatableReadOnlyList<T> Empty = new(new List<T>());

    public EquatableReadOnlyList(IReadOnlyList<T> list, IEqualityComparer<T>? comparer = null)
    {
        _list = list ?? throw new ArgumentNullException(nameof(list));
        _comparer = comparer ?? EqualityComparer<T>.Default;
    }

    public IEqualityComparer<T> Comparer => _comparer;

    public int Count => _list.Count;

    public T this[int index] => _list[index];

    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_list).GetEnumerator();

    public override bool Equals(object? obj) => Equals(obj as EquatableReadOnlyList<T>);

    public bool Equals(EquatableReadOnlyList<T>? other) => ReferenceEquals(this, other) || other is not null && this.SequenceEqual(other, _comparer);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        for (int i = 0; i < _list.Count; i++)
        {
            hash.Add(_list[i], _comparer);
        }
        return hash.ToHashCode();
    }
}