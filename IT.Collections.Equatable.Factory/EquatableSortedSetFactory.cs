using IT.Collections.Factory;
using IT.Collections.Factory.Factories;
using System;

namespace IT.Collections.Equatable.Factory;

public class EquatableSortedSetFactory : SortedSetFactory
{
    public override Type EnumerableType => typeof(EquatableSortedSet<>);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableSortedSet<T> Empty<T>(in Comparers<T> comparers = default) => new(comparers.Comparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableSortedSet<T> New<T>(int _, in Comparers<T> comparers = default) => new(comparers.Comparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableSortedSet<T> New<T>(int capacity, EnumerableBuilder<T> builder, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.Comparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var sortedSet = new EquatableSortedSet<T>(comparers.Comparer);

        builder(sortedSet.Add);

        return sortedSet;
    }

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableSortedSet<T> New<T, TState>(int capacity, EnumerableBuilder<T, TState> builder, in TState state, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.Comparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var sortedSet = new EquatableSortedSet<T>(comparers.Comparer);

        builder(sortedSet.Add, in state);

        return sortedSet;
    }

#if !NET5_0_OR_GREATER
    protected override System.Collections.Generic.SortedSet<T> NewSet<T>(int capacity, in Comparers<T> comparers)
        => new EquatableSortedSet<T>(comparers.Comparer);
#endif
}